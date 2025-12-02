// Garante que o código só rode depois que a página estiver carregada
document.addEventListener("DOMContentLoaded", function () {

    var navbar = document.getElementById("mainNavbar");

    if (navbar) {
        window.addEventListener("scroll", function () {
            // Se rolar mais que 50px, vira ilha
            if (window.scrollY > 50) {
                navbar.classList.add("scrolled-mode");
            } else {
                navbar.classList.remove("scrolled-mode");
            }
        });
    } else {
        console.error("Navbar não encontrada! Verifique se o ID 'mainNavbar' está na tag nav.");
    }

});