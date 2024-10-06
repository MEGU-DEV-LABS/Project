const MenuBtn = document.querySelector('.burger__btn');
const MenuNav = document.querySelector('.nav__menu');

MenuBtn.addEventListener('click', ()=> {
   MenuNav.classList.toggle('nav__menu--active')
})
