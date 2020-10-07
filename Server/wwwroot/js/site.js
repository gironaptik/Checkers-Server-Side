// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function fnSaveData() {
    window.localStorage.setItem("CustName", document.getElementById("txtCustomer").value);
    window.localStorage.setItem("CustAddr", document.getElementById("txtCustAddr").value);

} 

function fnGetData() {
    alert(window.localStorage.getItem("CustName"));
    alert(window.localStorage.getItem("CustAddr"));
}   