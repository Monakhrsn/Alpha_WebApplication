const modals = document.querySelectorAll('[data-type="modal"]')

document.addEventListener("DOMContentLoaded", () => {
    const previewSize = 150
    
    let addProjectDescriptionQuill = null;
    let addProjectDescriptionTextarea = null;
    let editProjectDescriptionQuill = null;
    let editProjectDescriptionTextarea = null;
    
    // header dropdowns 
    const dropdowns = document.querySelectorAll('[data-type="dropdown"]');
    document.addEventListener('click', function (event) {
        let clickedDropdown = null
        dropdowns.forEach(dropdown => {
            const targetId = dropdown.getAttribute('data-target')
            if (!targetId || !targetId.startsWith('#')) return;

            const targetElement = document.querySelector(targetId)
            if (!targetElement) return;

            if (dropdown.contains(event.target)) {
                clickedDropdown = targetElement

                document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
                    if (openDropdown !== targetElement) {
                        openDropdown.classList.remove('dropdown-show')
                    }
                })

                targetElement.classList.toggle('dropdown-show')
            }
        })
    })

    // clients dropdown
    document.querySelectorAll('.form-select').forEach(select => {
        const trigger = select.querySelector('.form-select-trigger');
        const triggerText = trigger.querySelector('.form-select-text');
        const options = select.querySelectorAll('.form-select-option');
        const optionsContainer = select.querySelector('.form-select-options');
        const hiddenInput = select.querySelector('input[type="hidden"]');
        const placeholder = select.dataset.placeholder || "Choose"
        
        const setValue = (value = "", text = placeholder) => {
            triggerText.textContent = text
            hiddenInput.value = value
            select.classList.toggle('has-placeholder', !value)
        };
        
        setValue()

        trigger.addEventListener('click', (e) => {
            e.stopPropagation();
            
            document.querySelectorAll('.form-select-options.show')
                .forEach(el => el !== optionsContainer && el.classList.remove('show'))
            optionsContainer.classList.toggle('show');
        });

        options.forEach(option =>
            option.addEventListener('click', (e) => {
                setValue(option.dataset.value, option.textContent.trim())
                optionsContainer.classList.remove('show')
            })
        )
        
        document.addEventListener('click', (e) => {
            if (!select.contains(e.target)) {
                optionsContainer.classList.remove('show');
            }
        });
    });
    
    // open modal
    const modalButtons = document.querySelectorAll('[data-type="modal"]')
    
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)
            
            if(modal)
                modal.style.display = 'flex';
            
            if (!editProjectDescriptionQuill) {
                editProjectDescriptionTextarea = document.getElementById('edit-project-description');
                editProjectDescriptionQuill = new Quill('#edit-project-description-wysiwyg-editor', {
                    modules: {
                        syntax: true,
                        toolbar: '#edit-project-description-wysiwyg-toolbar',
                    },
                    theme: 'snow',
                    placeholder: 'Type something.. .'
                })

                editProjectDescriptionQuill.on('text-change', function () {
                    if (editProjectDescriptionTextarea)
                        editProjectDescriptionTextarea.value = editProjectDescriptionQuill.root.innerHTML;
                })
            }
            
            if (button.classList.contains('btn-edit-project')) {
                debugger
                modal.querySelector('input[name="ProjectName"]').value = button.dataset.projectname || '';
                modal.querySelector('input[name="ClientId"]').value = button.dataset.clientid || '';
                modal.querySelector('input[name="StartDate"]').value = button.dataset.startdate || '';
                modal.querySelector('input[name="EndDate"]').value = button.dataset.enddate || '';
                modal.querySelector('input[name="Budget"]').value = button.dataset.budget || '';
                modal.querySelector('input[name="Id"]').value = button.dataset.id || '';
                
                const imagePreview = modal.querySelector('.image-preview');
                if (imagePreview && button.dataset.image) {
                    imagePreview.src = button.dataset.image;
                }
                
                // Set Client Select visible text
                const clientInput = modal.querySelector('input[name="ClientId"]');
                if (clientInput) {
                    const clientSelect = clientInput.closest('.form-select');
                    const triggerText = clientSelect.querySelector('.form-select-text');
                    const selectedOption = clientSelect.querySelector(`.form-select-option[data-value="${button.dataset.clientid}"]`);

                    if (selectedOption) {
                        triggerText.textContent = selectedOption.textContent.trim();
                        clientInput.value = selectedOption.dataset.value;
                    }
                }


                if (editProjectDescriptionQuill) {
                    editProjectDescriptionQuill.root.innerHTML = button.dataset.description || '';
                    if (editProjectDescriptionTextarea) {
                        editProjectDescriptionTextarea.value = button.dataset.description || '';
                    }
                }
            }
            
            if (!addProjectDescriptionQuill) {
                addProjectDescriptionTextarea = document.getElementById('add-project-description');
                addProjectDescriptionQuill = new Quill('#add-project-description-wysiwyg-editor', {
                    modules: {
                        syntax: true,
                        toolbar: '#add-project-description-wysiwyg-toolbar',
                    },
                    theme: 'snow',
                    placeholder: 'Type something.. .'
                })
                
                addProjectDescriptionQuill.on('text-change', function () {
                    if (addProjectDescriptionTextarea)
                        addProjectDescriptionTextarea.value = addProjectDescriptionQuill.root.innerHTML;
                })
            }
        })
    })

    // Close modal
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none'
                
                // clear formdata
                modal.querySelectorAll('form').forEach(form => {
                    form.reset()
                    
                    const imagePreview = form.querySelector('.image-preview')
                    if (imagePreview)
                        imagePreview.src =''
                    
                    const imagePreviewer = form.querySelector('.image-previewer')
                    if (imagePreviewer)
                        imagePreviewer.classList.remove('selected')
                })
            }
        })
    })

    // handle image previewer
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')
        
        previewer.addEventListener('click', () => fileInput.click())
        
        fileInput.addEventListener('change', ({ target: { files } }) => {
          const file = files[0]
          if (file)
              processImage(file, imagePreview, previewer, previewSize)
        })
    })
    
    // handle submit forms in modals
    const forms = document.querySelectorAll('form#modal-form')
    
    forms.forEach(form => {
        form.addEventListener('submit', async(e) => {
            e.preventDefault()
            clearErrorMessages(form)
            
            const formData = new FormData(form)
            
            try {
                const res = await fetch(form.action, {
                    method: 'POST',
                    body: formData,
                })
                
                if (res.ok) {
                   const modal = form.closest('.modal') 
                    if (modal)
                        modal.style.display = 'none';
                    
                    window.location.reload()
                } 
                else if (res.status === 400) {
                    const data = await res.json()
                    
                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            let input = form.querySelector(`input[name="${key}"]`)
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
                            if (span) {
                                span.innerText = data.errors[key].join('\n')
                                span.classList.add('field-validation-error')
                            }
                        })
                    }
                }
            }
            catch (error) {
                console.log('Error submitting the form')
            }
        })
    })
})

function clearErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}

async function loadImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()
        
        reader.onerror = () => reject(new Error("Failed to load file."))
        reader.onload = (e) => {
            const img = new Image()
            img.onerror = () => reject(new Error("Filled to load image."))
            img.onload = () => resolve(img)
            img.src = e.target.result
        }
        
        reader.readAsDataURL(file)
    })
}

async function processImage(file, imagePreview, previewer, previewSize = 150) {
    try {
        const img = await loadImage(file)
        const canvas = document.createElement('canvas')
        canvas.width = previewSize
        canvas.height = previewSize
        
        const ctx = canvas.getContext('2d')
        ctx.drawImage(img, 0, 0, previewSize, previewSize)
        imagePreview.src = canvas.toDataURL('image/jpeg')
        previewer.classList.add('selected')
    } catch (error){
        console.error('Failed on image processing', error)
    }
}