// Add binding and such when document is loaded.
$(function () {
    // Here's my data model
    var viewModel = function () {
        this.loginViewModel = new LoginViewModel();
    };

    this.viewModel = new viewModel();

    ko.applyBindings(this.viewModel);
});