// Here's my data model
var ViewModel = function (competitions) {
    this.competitions = ko.observableArray(competitions);
    this.selectedCompetition = ko.observable('');
    
    this.selectedCompetitionName = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().Name;
    }, this);
    this.selectedCompetitionGuid = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().Guid;
    }, this);

    this.selectCompetitionOnServer = function () {
        window.location.href = "/home/index/" + this.selectedCompetition().Guid;
    };
};

var competitionsApi = "/api/competitions";

$.getJSON(competitionsApi, function (data) {
    ko.applyBindings(new ViewModel(data));
});