mergeInto(LibraryManager.library, {
    FocusUnityInputField: function (elementId) {
        var strTest = UTF8ToString("testing jslib print")
        console.log(strTest)
        var elID = UTF8ToString(elementId)
        console.log(elID)
        var inputElement = document.getElementById(elementId);
        
        if (inputElement) {
            console.log("testing inpu element active ")
            inputElement.style.display='None'
            

            inputElement.focus();
        }
    }
});

