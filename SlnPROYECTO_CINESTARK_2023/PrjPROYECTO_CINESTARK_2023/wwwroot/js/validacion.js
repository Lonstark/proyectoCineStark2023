
const nomUsuInput = document.getElementById('nomUsu');
const apeUsuInput = document.getElementById('apeUsu');
const emailInput = document.getElementById('email');
const contraInput = document.getElementById('contra');
const nroDocInput = document.getElementById('nroDoc');
const celularInput = document.getElementById('celular');
const submitButton = document.getElementById('btnlogin');

const nombreRegex = /^[a-zA-Z\s]{2,}$/;
const apellidoRegex = /^[a-zA-Z\s]{2,}$/;
const emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
const contraRegex = /^[a-zA-Z\d]{8,}$/;
const nroDocRegex = /^\d{8}$/;
const celularRegex = /^9\d{8}$/;

nomUsuInput.addEventListener('input', () => {
    if (!nombreRegex.test(nomUsuInput.value)) {
        nomError.textContent = 'Solo letras.';
    } else {
        nomError.textContent = '';
    }
});

apeUsuInput.addEventListener('input', () => {
    if (!apellidoRegex.test(apeUsuInput.value)) {
        apeError.textContent = 'Solo letras.';
    } else {
        apeError.textContent = '';
    }
});

emailInput.addEventListener('input', () => {
    if (!emailRegex.test(emailInput.value)) {
        emailError.textContent = 'Correo inv\u00e1lido';
    } else {
        emailError.textContent = '';
    }
});

contraInput.addEventListener('input', () => {
    if (!contraRegex.test(contraInput.value)) {
        contraError.textContent = 'La contrase\u00f1a debe tener al menos 8 caracteres.';
    } else {
        contraError.textContent = '';
    }
});

nroDocInput.addEventListener('input', () => {
    if (!nroDocRegex.test(nroDocInput.value)) {
        docError.textContent = 'Solo 8 letras.';
    } else {
        docError.textContent = '';
    }
});

celularInput.addEventListener('input', () => {
    if (!celularRegex.test(celularInput.value)) {
        celularError.textContent = 'Solo 9 dígitos, empezando por el 9.';
    } else {
        celularError.textContent = '';
    }
});
