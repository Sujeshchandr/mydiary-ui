
var DRDateExtension = (function () {

	//extension method of Date Object to return formatted date 
	Date.prototype.getFormattedDate = function (format) {

		var formattedDate;
		var year = this.getFullYear();
		var month = (1 + this.getMonth()).toString();
		month = month.length > 1 ? month : '0' + month;
		var day = this.getDate().toString();
		day = day.length > 1 ? day : '0' + day;

		switch (format) {
			case 'dd/MM/yyyy':
				formattedDate = day + '/' + month + '/' + year;
				break;
		}
		return formattedDate;
	}
})();
