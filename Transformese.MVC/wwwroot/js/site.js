document.addEventListener("DOMContentLoaded", function () {

    var navbar = document.getElementById("mainNavbar");
    if (navbar) {
        window.addEventListener("scroll", function () {
            if (window.scrollY > 50) {
                navbar.classList.add("scrolled-mode");
            } else {
                navbar.classList.remove("scrolled-mode");
            }
        });
    }

    // --- NOVO: LÓGICA DO DARK MODE ---
    const toggleBtn = document.getElementById('darkModeToggle');
    const htmlElement = document.documentElement;
    const icon = toggleBtn ? toggleBtn.querySelector('i') : null;

    // 1. Verifica se já existe preferência salva
    const savedTheme = localStorage.getItem('theme') || 'light';
    htmlElement.setAttribute('data-theme', savedTheme);
    updateIcon(savedTheme);

    // 2. Ação do Clique
    if (toggleBtn) {
        toggleBtn.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';

            // Aplica e Salva
            htmlElement.setAttribute('data-theme', newTheme);
            localStorage.setItem('theme', newTheme);
            updateIcon(newTheme);
        });
    }

    // 3. Função para trocar o ícone (Lua <-> Sol)
    function updateIcon(theme) {
        if (!icon) return;

        if (theme === 'dark') {
            icon.classList.remove('bi-moon-fill');
            icon.classList.add('bi-sun-fill');
            icon.style.color = "#ffd700"; // Amarelo (Sol)
        } else {
            icon.classList.remove('bi-sun-fill');
            icon.classList.add('bi-moon-fill');
            icon.style.color = ""; // Volta para a cor original (definida no CSS)
        }
    }
});