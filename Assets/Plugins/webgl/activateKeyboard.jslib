
mergeInto(LibraryManager.library, {
    FocusUnityInputField: function (unityObjectName, unityCallbackMethod) {
        console.log("Focusing hidden input field");

        // Create a hidden input element if it doesn't already exist
        var hiddenInput = document.getElementById('hiddenInputForKeyboard');
        if (hiddenInput== null) {
            console.log('creating input field')
            hiddenInput = document.createElement('input');
            hiddenInput.type = 'text';
            hiddenInput.id = 'hiddenInputForKeyboard';
            hiddenInput.style.position = 'absolute';
            hiddenInput.style.opacity = 0;
            hiddenInput.style.height = 0;
            hiddenInput.style.width = 0;
            hiddenInput.style.border = 'none';
            document.body.appendChild(hiddenInput);

            hiddenInput.addEventListener('input', function () {
                // Send the value of the hidden input to Unity
                console.log("Text changed: ")
                var objName =  UTF8ToString(unityObjectName)
                var objCallback =  UTF8ToString(unityCallbackMethod)
                
                

                window.unityInstance.SendMessage(objName, objCallback, hiddenInput.value);
            });
        }

        // Focus the hidden input element to trigger the keyboard
        hiddenInput.focus();
        console.log("Hidden input focused.");
    }
});