﻿/***
 * Copyright 2012 LTN Consulting, Inc. /dba Digital Primates®
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * @author Michael Labriola <labriola@digitalprimates.net>
 */

using SharpKit.Html;
using guice;
using randori.content;
using randori.styles;

namespace randori.startup {
    public class RandoriApplication {
        Node rootNode;

        public void launch() {
            //Create the injector that will be used in the application
            var guiceJs = new GuiceJs();
            var injector = guiceJs.createInjector(new RandoriModule());

            //Load the style sheets and extract our needed information
            //var styleManager = (StyleBehaviorManager)injector.getInstance(typeof(StyleBehaviorManager));
            //styleManager.parseAndReleaseNodes(J("link"));

            var domWalker = (DomWalker)injector.getInstance(typeof(DomWalker));
            domWalker.walkDomFragment(rootNode);
        }

        public RandoriApplication(Node rootNode) {
            this.rootNode = rootNode;
        }
    }
}