
var upload = (function () {

    var viewModel = {
        dropDownOptions: ko.observableArray(
            [
                { id: 1, Text: "Manusript 1" },
                { id: 2, Text: "Manusript 2" },
                { id: 3, Text: "Manusript 3" },
                { id: 4, Text: "Manusript 4" },
                { id: 5, Text: "Manusript 5" },
                { id: 6, Text: "Manusript 6" },
                { id: 7, Text: "Manusript 7" },
                { id: 8, Text: "Manusript 8" }
            ]),

        fileFormats: ko.observableArray(
          [
              { id: 1, Text: "PDF" },
              { id: 2, Text: "JPEG" },
              { id: 3, Text: "Manusript 3" },
              { id: 4, Text: "Manusript 4" },
              { id: 5, Text: "Manusript 5" },
              { id: 6, Text: "Manusript 6" },
              { id: 7, Text: "Manusript 7" },
              { id: 8, Text: "Manusript 8" }
          ]),

        contentTypes: ko.observableArray(
          [
              { id: 1, Text: "Manusript 1" },
              { id: 2, Text: "Manusript 2" },
              { id: 3, Text: "Manusript 3" },
              { id: 4, Text: "Manusript 4" },
              { id: 5, Text: "Manusript 5" },
              { id: 6, Text: "Manusript 6" },
              { id: 7, Text: "Manusript 7" },
              { id: 8, Text: "Manusript 8" }
          ]),
        selectedValue : ko.observable(),    
        contentTypes: ko.observableArray([{ id: 1, Code: "Manusript" }]),
        add: function () {
            this.contentTypes.push({ id: 2, Code: "Figure" })
        },
        afterRenderingOption: function (a,b) {
            console.log(a);
            console.log(b);
        }
    };

    ko.applyBindings(viewModel);

    return
    {

    };

})();
