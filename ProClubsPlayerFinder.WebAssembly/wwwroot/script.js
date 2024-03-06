function setInputValueById(id, newString) {
    var inputElement = document.getElementById(id);
    if (inputElement) {
        inputElement.value = newString;
    }
}