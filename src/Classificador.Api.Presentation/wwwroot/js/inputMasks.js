// const handlePhone = (event) => {
//     let input = event.target
//     input.value = phoneMask(input.value)
//   }
  
//   const phoneMask = (value) => {
//     if (!value) return ""
//     value = value.replace(/\D/g,'')
//     value = value.replace(/(\d{2})(\d)/,"($1) $2")
//     value = value.replace(/(\d)(\d{4})$/,"$1-$2")
//     return value
//   }

function maskTelephone(input) {
    var value = input.value;
    value = value.replace(/\D/g, "");
    if (value.length > 11) value = value.substr(0, 11);
    value = value.replace(/^(\d{2})(\d)/g, "($1) $2");
    value = value.replace(/(\d)(\d{4})$/, "$1-$2");
    input.value = value;
}

function applyMasks() {
    const inputs = document.querySelectorAll('input[data-mask]');
    inputs.forEach(input => {
        const maskType = input.getAttribute('data-mask');
        input.addEventListener('input', () => {
            switch (maskType) {
                case 'tel':
                    maskTelephone(input);
                    break;
            }
        });
    });
}

document.addEventListener('DOMContentLoaded', applyMasks);