using SharpKit.JavaScript;
using SharpKit.jQuery;
using randori.attributes;
using randori.template;

namespace randori.behaviors {
    [JsType(JsMode.Prototype, Export=false)]
    public delegate void ListChangeDelegate(int index, JsObject data);

    [JsType(JsMode.Prototype, NativeOverloads = false, OmitCasts = true)]
    public class List : SimpleList {
        public ListChangeDelegate listChanged;

        //TODO remove me when ben fixes plugin
        [View(required = false)]
        private jQuery template;

        //TODO remove me when ben fixes plugin
        [Inject]
        public TemplateBuilder templateBuilder1;

        public object selectedItem {
            get { return _data[_selectedIndex]; }
            set {
                if (_data == null) {
                    return;
                }

                for (int i = 0; i < _data.length; i++) {
                    if (value == _data[i]) {
                        selectedIndex = i;
                        break;
                    }
                }
            }
        }

        protected int _selectedIndex;
        public int selectedIndex {
            get { return _selectedIndex; }
            set {
                _selectedIndex = value;
                rootElement.children().removeClass("selected");
                if (value > -1 && value < rootElement.children().length) {
                    rootElement.children().eq(value).addClass("selected");
                    if (listChanged != null) {
                        listChanged(value, data[value].As<JsObject>());
                    }
                }
            }
        }

        protected override void onRegister() {
            //TODO remove me when ben fixes plugin
            this.templateBuilder = templateBuilder1;

            base.onRegister();

            //adds a listener to the root element
            //fires click whenever a .listItem is clicked
            rootElement.@delegate(".randoriListItem", "click", onItemClick);
        }

        public override void renderList() {
            base.renderList();
            selectedIndex = 0;
        }

        protected void onItemClick(Event e) {
            jQuery targetJQ = jQueryContext.J(e.currentTarget);
            int index = targetJQ.index();
            selectedIndex = index;
        }

        public List()
            : base() {
        }
    }
}