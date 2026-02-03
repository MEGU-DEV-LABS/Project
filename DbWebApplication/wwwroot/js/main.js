// Основна форма входу (не адміністраторська)
const form = document.getElementById("form");
const email = document.getElementById("email");
const password = document.getElementById("password");
const name = document.getElementById("first__name");
const second__name = document.getElementById("second__name");
const telephone = document.getElementById("telephone");

const form__login = document.querySelector(".form__login");
const email__login__form = document.getElementById("email__login");
const password__login__form = document.getElementById("password__login");

const form__subject = document.getElementById("form__subject");
const form__subject__name = document.getElementById("name__subject");

// Форма реєстрації адміністратора
const admin__form = document.getElementById("admin__form");
const admin__name = document.getElementById("admin__first__name");
const admin__second__name = document.getElementById("admin__second__name");
const admin__third__name = document.getElementById("admin__father__name");
const admin__phone = document.getElementById("admin__phone");
const admin__email = document.getElementById("email__admin");
const admin__password = document.getElementById("admin__password");
const password__admin__cheack = document.getElementById(
  "password__admin__cheack"
);

const admin__form__setings = document.getElementById("admin__form__setings");
const admin__first__name__setings = document.getElementById(
  "admin__first__name__setings"
);
const admin__second__name__setings = document.getElementById(
  "admin__second__name__setings"
);
const admin__father__name__setings = document.getElementById(
  "admin__father__name__setings"
);
const admin__phone__setings = document.getElementById("admin__phone__setings");
const email__admin__setings = document.getElementById("email__admin__setings");

const new__faculty = document.getElementById("form__new__faculty");
const new__work = document.getElementById("form__new__work");

const toggleBtn = document.getElementById("toggleSidebar");
const sidebar = document.querySelector(".sidebar");

const forgetform = document.getElementById("forget__form");

if (form) {
  form.addEventListener("submit", (e) => {

    validateInputs();
  });
}

if (forgetform) {
  forgetform.addEventListener("submit", (e) => {

  });
}
if (new__faculty) {
  new__faculty.addEventListener("submit", (e) => {

  });
}
if (new__work) {
  new__work.addEventListener("submit", (e) => {

  });
}

if (admin__form__setings) {
  admin__form__setings.addEventListener("submit", (e) => {

    validateInputsAdminSatinggs();
  });
}
if (form__subject) {
  form__subject.addEventListener("submit", (e) => {
    validateInputsSubject();
  });
}

const validateInputsAdminSatinggs = () => {
  const admin__first__name__setingsValue =
    admin__first__name__setings.value.trim();
  const admin__second__name__setingsValue =
    admin__second__name__setings.value.trim();
  const admin__father__name__setingsValue =
    admin__father__name__setings.value.trim();
  const admin__phone__setingsValue = admin__phone__setings.value.trim();
  const email__admin__setingsValue = email__admin__setings.value.trim();

  if (email__admin__setingsValue === "") {
    setError__SubjectAdminSetings(
      email__admin__setings,
      "Пошта є обов’язковою"
    );
  } else if (!isValidEmail(email__admin__setingsValue)) {
    setError__SubjectAdminSetings(email__admin__setings, "Некоректний email");
  } else {
    setSuccessSubjectAdminSetings(email__admin__setings);
  }

  if (admin__phone__setingsValue === "") {
    setError__SubjectAdminSetings(
      admin__phone__setings,
      "Телефон є обов’язковий"
    );
  } else if (admin__phone__setingsValue.length < 12) {
    setError__SubjectAdminSetings(
      admin__phone__setings,
      "Телефон має бути не менш 12 цифр"
    );
  } else {
    setSuccessSubjectAdminSetings(admin__phone__setings);
  }

  if (admin__first__name__setingsValue === "") {
    setError__SubjectAdminSetings(
      admin__first__name__setings,
      "Ім`я є обов’язковою"
    );
  } else {
    setSuccessSubjectAdminSetings(admin__first__name__setings);
  }

  if (admin__second__name__setingsValue === "") {
    setError__SubjectAdminSetings(
      admin__second__name__setings,
      "Прізвище є обов’язковою"
    );
  } else {
    setSuccessSubjectAdminSetings(admin__second__name__setings);
  }

  if (admin__father__name__setingsValue === "") {
    setError__SubjectAdminSetings(
      admin__father__name__setings,
      "Ім’я по батькові"
    );
  } else {
    setSuccessSubjectAdminSetings(admin__father__name__setings);
  }
};

const setSuccessSubjectAdminSetings = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const setError__SubjectAdminSetings = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const validateInputsSubject = () => {
  const form__subject__nameValue = form__subject__name.value.trim();

  if (form__subject__nameValue === "") {
    setError__Subject(form__subject__name, "напишіть назву предмета");
  } else {
    setSuccessSubject(form__subject__name);
  }
};

const setSuccessSubject = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const setError__Subject = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

if (admin__form) {
  admin__form.addEventListener("submit", (e) => {

    validateInputsAdmin();
  });
}

if (form__login) {
  form__login.addEventListener("submit", (e) => {
    validateInputsLogin();
  });
}

const setError__login = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const setSuccessLogin = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const validateInputsLogin = () => {
  const emailLoginValue = email__login__form.value.trim();
  const passwordLoginValue = password__login__form.value.trim();

  if (emailLoginValue === "") {
    setError__login(email__login__form, "Пошта є обов’язковою");
  } else if (!isValidEmail(emailLoginValue)) {
    setError__login(email__login__form, "Некоректний email");
  } else {
    setSuccessLogin(email__login__form);
  }

  if (passwordLoginValue === "") {
    setError__login(password__login__form, "Пароль є обов’язковий");
  } else if (passwordLoginValue.length < 8) {
    setError__login(password__login__form, "Пароль має бути не менш 8 цифр");
  } else {
    setSuccessLogin(password__login__form);
  }
};

const setError = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const setSuccess = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const isValidEmail = (email) => {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(String(email).toLowerCase());
};

const validateInputs = () => {
  const emailValue = email.value.trim();
  const passwordValue = password.value.trim();
  const nameValue = name.value.trim();
  const second__name__value = second__name.value.trim();
  const telephone__value = telephone.value.trim();

  if (emailValue === "") {
    setError(email, "Пошта є обов’язковою");
  } else if (!isValidEmail(emailValue)) {
    setError(email, "Некоректний email");
  } else {
    setSuccess(email);
  }

  if (nameValue === "") {
    setError(name, "ім’я є обов’язковим");
  } else {
    setSuccess(name);
  }
  if (second__name__value === "") {
    setError(second__name, "Призвіще є обов’язкова");
  } else {
    setSuccess(second__name);
  }
  if (telephone__value === "") {
    setError(telephone, "Телефон є обов’язковий");
  } else if (telephone__value.length < 12) {
    setError(telephone, "Телефон має бути не менш 12 цифр");
  } else {
    setSuccess(telephone);
  }

  if (passwordValue === "") {
    setError(password, "Пароль є обов’язковий");
  } else if (passwordValue.length < 8) {
    setError(password, "Пароль має бути не менш 8 цифр");
  } else {
    setSuccess(password);
  }
};

const setError__admin = (element, message) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = message;
  inputControl.classList.add("error");
  inputControl.classList.remove("success");
};

const setSuccessAdmin = (element) => {
  const inputControl = element.parentElement;
  const errorDisplay = inputControl.querySelector(".error");

  errorDisplay.innerText = "";
  inputControl.classList.add("success");
  inputControl.classList.remove("error");
};

const validateInputsAdmin = () => {
  const name__admin__value = admin__name.value.trim();
  const second__name__admin__value = admin__second__name.value.trim();
  const third__name__admin__value = admin__third__name.value.trim();

  const email__admin__value = admin__email.value.trim();
  const password__admin__value = admin__password.value.trim();
  const password__c__admin__value = password__admin__cheack.value.trim();
  const admin__phone__value = admin__phone.value.trim();

  if (name__admin__value === "") {
    setError__admin(admin__name, "Заповніть це поле");
  } else {
    setSuccessAdmin(admin__name);
  }
  if (admin__second__name.value === "") {
    setError__admin(admin__second__name, "Заповніть це поле");
  } else {
    setSuccessAdmin(admin__second__name);
  }

  if (admin__third__name.value === "") {
    setError__admin(admin__third__name, "Заповніть це поле");
  } else {
    setSuccessAdmin(admin__third__name);
  }

  if (admin__phone.value === "") {
    setError__admin(admin__phone, "Заповніть це поле");
  } else if (admin__phone__value.length < 12) {
    setError__admin(admin__phone, "телефон має бути не менш 12 цифр");
  } else {
    setSuccessAdmin(admin__phone);
  }

  if (email__admin__value === "") {
    setError__admin(admin__email, "Пошта є обов’язковою");
  } else if (!isValidEmail(email__admin__value)) {
    setError__admin(admin__email, "Некоректний email");
  } else {
    setSuccessAdmin(admin__email);
  }

  if (password__admin__value === "") {
    setError__admin(admin__password, "Пароль обов’язковий");
  } else if (password__admin__value.length < 8) {
    setError__admin(admin__password, "Пароль має бути не менш 8 цифр");
  } else {
    setSuccessAdmin(admin__password);
  }

  if (password__c__admin__value === "") {
    setError__admin(
      password__admin__cheack,
      "Підтвердження пароля обов’язкове"
    );
  } else if (password__c__admin__value !== password__admin__value) {
    setError__admin(password__admin__cheack, "Паролі не збігаються");
  } else {
    setSuccessAdmin(password__admin__cheack);
  }
};

const qrButton = document.querySelector(".qr__code__btn");
if (qrButton) {
  qrButton.addEventListener("click", function () {
    const uploadFileBlock = document.querySelector(".upload__file");
    if (uploadFileBlock) {
      uploadFileBlock.style.display =
        uploadFileBlock.style.display === "none" ? "block" : "none";
    }
  });
}

if (toggleBtn) {
  toggleBtn.addEventListener("click", () => {
    sidebar.classList.toggle("closed");
  });
}

if (admin__phone) {
  const im = new Inputmask("+380 (99) 999-99-99");
  im.mask(admin__phone);
}

if (telephone) {
  const im = new Inputmask("+380 (99) 999-99-99");
  im.mask(telephone);
}

if (admin__phone__setings) {
  const im = new Inputmask("+380 (99) 999-99-99");
  im.mask(admin__phone__setings);
}

document.addEventListener("click", (e) => {
  const isDropdownButton = e.target.matches("[data-dropdown-button]");
  if (!isDropdownButton && e.target.closest("[data-dropdown]") != null) return;

  let currentDropdown;
  if (isDropdownButton) {
    currentDropdown = e.target.closest("[data-dropdown]");
    currentDropdown.classList.toggle("active");
  }

  document.querySelectorAll("[data-dropdown].active").forEach((dropdown) => {
    if (dropdown === currentDropdown) return;
    dropdown.classList.remove("active");
  });
});
