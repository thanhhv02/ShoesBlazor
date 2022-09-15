CKEditorInterop = (() => {
    
    var editors = {};

    return {
        init(id, dotNetReference) {
            window.ClassicEditor
                .create(document.getElementById(id))
                .then(editor => {
                    editors[id] = editor;
                    editor.model.document.on('change:data', () => {
                        var data = editor.getData();

                        var el = document.createElement('div');
                        el.innerHTML = data.replace(/<p[^>]*>/g, '').replace(/<\/p>/g, '');
                        if (el.innerText.trim() === '')
                            data = null;

                        dotNetReference.invokeMethodAsync('EditorDataChanged', data.replace(/<p[^>]*>/g, '').replace(/<\/p>/g, ''));
                    });
                })
                .catch(error => console.error(error));
        },
        destroy(id) {
            editors[id].destroy()
                .then(() => delete editors[id])
                .catch(error => console.log(error));
        }
    };
})();

CKEditorInterop.editorConfig = function (config) {
    config.language = 'en';
    enterMode: CKEDITOR.ENTER_BR;
};

