// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Add the code for the toggle menu

// Step 1: select the element the user will click on to make this menu show/hide. In this case it's the toggle-icon and since we are grabbing it by it's classname we need to include the period 
const clickButton = document.querySelector('.toggle-btn');

// Step 2: add a click event to that icon
clickButton.addEventListener('click', () => {
    // when the icon clicked, we are going to grab the nav elementy and
    // add or remove (show/hide) the menu
    document.querySelector('nav').classList.toggle('show-nav')
});