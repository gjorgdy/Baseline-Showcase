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

// ParagraphForm
(function() {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['ParagraphForm'] = template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
            var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "﻿<label for=\"title\">Title</label>\r\n<input type=\"text\" name=\"title\" id=\"title\" maxlength=\"32\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"title") || (depth0 != null ? lookupProperty(depth0,"title") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"title","hash":{},"data":data,"loc":{"start":{"line":2,"column":65},"end":{"line":2,"column":74}}}) : helper)))
                + "\">\r\n<label for=\"paragraph\">Paragraph</label>\r\n<textarea name=\"paragraph\" id=\"paragraph\" cols=\"64\" rows=\"8\" maxlength=\"256\">\r\n  "
                + alias4(((helper = (helper = lookupProperty(helpers,"paragraph") || (depth0 != null ? lookupProperty(depth0,"paragraph") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"paragraph","hash":{},"data":data,"loc":{"start":{"line":5,"column":2},"end":{"line":5,"column":15}}}) : helper)))
                + "\r\n</textarea>";
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

// SkillsForm
(function() {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['SkillsForm'] = template({"1":function(container,depth0,helpers,partials,data) {
            var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "  <span class=\"name\">\r\n    <label for=\"name-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":12,"column":21},"end":{"line":12,"column":31}}}) : helper)))
                + "\">Name</label>\r\n    <input type=\"text\" name=\"name-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":13,"column":34},"end":{"line":13,"column":44}}}) : helper)))
                + "\" id=\"name-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":13,"column":55},"end":{"line":13,"column":65}}}) : helper)))
                + "\" maxlength=\"8\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"name") || (depth0 != null ? lookupProperty(depth0,"name") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"name","hash":{},"data":data,"loc":{"start":{"line":13,"column":88},"end":{"line":13,"column":96}}}) : helper)))
                + "\">\r\n  </span>\r\n  <span class=\"percentage\">\r\n    <label for=\"percentage-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":16,"column":27},"end":{"line":16,"column":37}}}) : helper)))
                + "\">Percentage</label>\r\n    <input type=\"number\" name=\"percentage-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":17,"column":42},"end":{"line":17,"column":52}}}) : helper)))
                + "\" id=\"percentage-"
                + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":17,"column":69},"end":{"line":17,"column":79}}}) : helper)))
                + "\" min=\"0\" max=\"100\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"percentage") || (depth0 != null ? lookupProperty(depth0,"percentage") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"percentage","hash":{},"data":data,"loc":{"start":{"line":17,"column":106},"end":{"line":17,"column":120}}}) : helper)))
                + "\">\r\n  </span>\r\n";
        },"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
            var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "﻿<label for=\"title\">Title</label>\r\n<input type=\"text\" name=\"title\" id=\"title\" maxlength=\"32\" value=\""
                + container.escapeExpression(((helper = (helper = lookupProperty(helpers,"title") || (depth0 != null ? lookupProperty(depth0,"title") : depth0)) != null ? helper : container.hooks.helperMissing),(typeof helper === "function" ? helper.call(alias1,{"name":"title","hash":{},"data":data,"loc":{"start":{"line":2,"column":65},"end":{"line":2,"column":74}}}) : helper)))
                + "\">\r\n\r\n<h3>Skills</h3>\r\n<span class=\"amount\">\r\n  <button type=\"button\" id=\"addSkill\">+</button>\r\n  <button type=\"button\" id=\"removeSkill\">-</button>\r\n</span>\r\n\r\n"
                + ((stack1 = lookupProperty(helpers,"each").call(alias1,(depth0 != null ? lookupProperty(depth0,"skills") : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":10,"column":0},"end":{"line":19,"column":9}}})) != null ? stack1 : "");
        },"useData":true});
})();

// TileForm
(function() {
    var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
    templates['TileForm'] = template({"1":function(container,depth0,helpers,partials,data) {
            return "    <h2>Edit tile</h2>\r\n";
        },"3":function(container,depth0,helpers,partials,data) {
            return "    <h2>Create tile</h2>\r\n";
        },"5":function(container,depth0,helpers,partials,data) {
            return "        <option value=\"none\" selected disabled hidden>Select a type</option>\r\n        <option value=\"paragraph\">Paragraph</option>\r\n        <option value=\"skills\">Skills</option>\r\n";
        },"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
            var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
                if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
                    return parent[propertyName];
                }
                return undefined
            };

            return "﻿<div class=\"modal-content\">\r\n"
                + ((stack1 = lookupProperty(helpers,"if").call(alias1,(depth0 != null ? lookupProperty(depth0,"type") : depth0),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.program(3, data, 0),"data":data,"loc":{"start":{"line":2,"column":2},"end":{"line":6,"column":9}}})) != null ? stack1 : "")
                + "  <input name=\"tileId\" type=\"hidden\" id=\"tileId\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"id") || (depth0 != null ? lookupProperty(depth0,"id") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"id","hash":{},"data":data,"loc":{"start":{"line":7,"column":56},"end":{"line":7,"column":62}}}) : helper)))
                + "\" />\r\n  <form id=\"tileDefaults\">\r\n    <label for=\"type\">Type</label>\r\n    <select name=\"type\" id=\"type\">\r\n"
                + ((stack1 = (lookupProperty(helpers,"select")||(depth0 && lookupProperty(depth0,"select"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"type") : depth0),{"name":"select","hash":{},"fn":container.program(5, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":11,"column":6},"end":{"line":15,"column":17}}})) != null ? stack1 : "")
                + "    </select>\r\n    <label for=\"width\">Width</label>\r\n    <input type=\"number\" name=\"width\" id=\"width\" min=\"1\" max=\"3\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"width") || (depth0 != null ? lookupProperty(depth0,"width") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"width","hash":{},"data":data,"loc":{"start":{"line":18,"column":72},"end":{"line":18,"column":81}}}) : helper)))
                + "\" />\r\n    <label for=\"height\">Height</label>\r\n    <input type=\"number\" name=\"height\" id=\"height\" min=\"1\" max=\"3\" value=\""
                + alias4(((helper = (helper = lookupProperty(helpers,"height") || (depth0 != null ? lookupProperty(depth0,"height") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"height","hash":{},"data":data,"loc":{"start":{"line":20,"column":74},"end":{"line":20,"column":84}}}) : helper)))
                + "\" />\r\n  </form>\r\n  <form id=\"tileAttributes\"></form>\r\n  <span id=\"modalError\"></span>\r\n  <span class=\"buttons\">\r\n     <button type=\"submit\" id=\"submitModal\">Submit</button>\r\n     <button type=\"button\" id=\"closeModal\" onclick=\"closeModal()\">Close</button>\r\n  </span>\r\n</div>";
        },"useData":true});
})();