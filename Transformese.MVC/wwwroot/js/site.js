// site-landing.js - Otimizado
const html = document.documentElement;
const toggle = document.getElementById("darkModeToggle");

// Função que aplica o tema e salva a preferência
function applyTheme(theme) {
    html.setAttribute("data-theme", theme);
    localStorage.setItem("theme", theme);
    updateIcon(theme);
}

// Função para atualizar o ícone com base no tema
function updateIcon(currentTheme) {
    // Bootstrap Icons para Lua (modo escuro) e Sol (modo claro)
    if (currentTheme === "dark") {
        toggle.classList.remove("bi-moon-stars-fill");
        toggle.classList.add("bi-sun-fill");
    } else {
        toggle.classList.remove("bi-sun-fill");
        toggle.classList.add("bi-moon-stars-fill");
    }
}

// Lógica de Detecção e Aplicação Imediata (para evitar FOUC - Flash of Unstyled Content)
(function initTheme() {
    const savedTheme = localStorage.getItem("theme");

    if (savedTheme) {
        // Se houver tema salvo, usa ele
        html.setAttribute("data-theme", savedTheme);
        updateIcon(savedTheme);
    } else {
        // Caso contrário, detecta a preferência do sistema
        const prefersDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
        const initialTheme = prefersDark ? "dark" : "light";
        html.setAttribute("data-theme", initialTheme);
        updateIcon(initialTheme);
    }
})();

// Event Listener para a troca de tema
if (toggle) {
    toggle.addEventListener("click", () => {
        const currentTheme = html.getAttribute("data-theme");
        const newTheme = currentTheme === "light" ? "dark" : "light";
        applyTheme(newTheme);
    });
}