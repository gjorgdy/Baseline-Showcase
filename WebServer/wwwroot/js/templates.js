// ParagraphTemplate
(function() {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['ParagraphTemplate'] = template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
            var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "﻿<h2>"
                + alias4(((helper = (helper = lookupProperty(helpers,"title") || (depth0 != null ? lookupProperty(depth0,"title") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"title","hash":{},"data":data,"loc":{"start":{"line":1,"column":5},"end":{"line":1,"column":14}}}) : helper)))
                + "</h2>\r\n<p>\r\n    "
                + alias4(((helper = (helper = lookupProperty(helpers,"paragraph") || (depth0 != null ? lookupProperty(depth0,"paragraph") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"paragraph","hash":{},"data":data,"loc":{"start":{"line":3,"column":4},"end":{"line":3,"column":17}}}) : helper)))
                + "\r\n</p>";
        },"useData":true});
})();

// SkillsTemplate
(function() {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['SkillsTemplate'] = template({"1":function(container,depth0,helpers,partials,data) {
            var alias1=container.lambda, alias2=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "    <text>\r\n      <div class=\"skill progress-bar\" role=\"progressbar\" aria-valuenow=\""
                + alias2(alias1((depth0 != null ? lookupProperty(depth0,"percentage") : depth0), depth0))
                + "\" aria-valuemin=\"0\" aria-valuemax=\"100\">\r\n        <div class=\"progress\" style=\"width: "
                + alias2(alias1((depth0 != null ? lookupProperty(depth0,"percentage") : depth0), depth0))
                + "%;\">\r\n        </div>\r\n        <span class=\"name\">"
                + alias2(alias1((depth0 != null ? lookupProperty(depth0,"name") : depth0), depth0))
                + "</span>\r\n        <span class=\"percentage\">"
                + alias2(alias1((depth0 != null ? lookupProperty(depth0,"percentage") : depth0), depth0))
                + "%</span>\r\n      </div>\r\n    </text>\r\n";
        },"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
            var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "﻿<h2>"
                + container.escapeExpression(((helper = (helper = lookupProperty(helpers,"title") || (depth0 != null ? lookupProperty(depth0,"title") : depth0)) != null ? helper : container.hooks.helperMissing),(typeof helper === "function" ? helper.call(alias1,{"name":"title","hash":{},"data":data,"loc":{"start":{"line":1,"column":5},"end":{"line":1,"column":14}}}) : helper)))
                + "</h2>\r\n"
                + ((stack1 = lookupProperty(helpers,"each").call(alias1,(depth0 != null ? lookupProperty(depth0,"skills") : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":2,"column":2},"end":{"line":11,"column":11}}})) != null ? stack1 : "");
        },"useData":true});
})();