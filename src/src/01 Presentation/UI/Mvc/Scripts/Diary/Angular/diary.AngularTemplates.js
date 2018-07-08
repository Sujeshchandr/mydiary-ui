//Template Modules --- put templates into template cache
angular.module("template/popover/popover-template.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/popover/popover-template.html",
      "<div class=\"popover\"\n" +
      "  tooltip-animation-class=\"fade\"\n" +
      "  tooltip-classes\n" +
      "  ng-class=\"{ in: isOpen() }\">\n" +
      "  <div class=\"arrow\"></div>\n" +
      "\n" +
      "  <div class=\"popover-inner\">\n" +
      "<button ng-click=\"closePopUp()\"  type=\"button\" class=\"close closeErrorLog\" aria-hidden=\"true\">X</button>" +
      "      <h3 class=\"popover-title customTitle1\" ng-bind=\"title\" ng-if=\"title\"></h3>\n" +
      "      <div class=\"popover-content\"\n" +
      "        tooltip-template-transclude=\"contentExp()\"\n" +
      "        tooltip-template-transclude-scope=\"originScope()\"></div>\n" +
      "  </div>\n" +
      "</div>\n" +
      "");
}]);

angular.module("template/popover/popover.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/popover/popover.html",
      "<div class=\"popover\"\n" +
      "  tooltip-animation-class=\"fade\"\n" +
      "  tooltip-classes\n" +
      "  ng-class=\"{ in: isOpen() }\">\n" +
      "  <div class=\"arrow\"></div>\n" +
      "\n" +
      "  <div class=\"popover-inner\">\n" +
      "<button ng-click=\"closePopUp()\"  type=\"button\" class=\"close closeErrorLog\" aria-hidden=\"true\">X</button>" +
      "      <h3 class=\"popover-title innerTitle\" ng-bind=\"title\" ng-if=\"title\"></h3>\n" +
      "      <div class=\"popover-content\" ng-bind=\"content\"></div>\n" +
      "  </div>\n" +
      "</div>\n" +
      "");
}]);

