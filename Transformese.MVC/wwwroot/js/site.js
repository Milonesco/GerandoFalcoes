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

    // Auto-hover nas logos do carrossel (detecta logos passando sob o cursor)
    const marqueeContainer = document.querySelector('.marquee-container');
    
    if (!marqueeContainer) return; // Sai se não encontrar o container

    const logos = marqueeContainer.querySelectorAll('.marquee-content img');
    let mouseX = null;
    let mouseY = null;

    // Captura a posição do mouse no container
    marqueeContainer.addEventListener('mousemove', function (e) {
        const rect = marqueeContainer.getBoundingClientRect();
        mouseX = e.clientX;
        mouseY = e.clientY - rect.top;
    });

    // Remove a posição quando o mouse sai do container
    marqueeContainer.addEventListener('mouseleave', function () {
        mouseX = null;
        mouseY = null;
        // Remove hover de todas as logos
        logos.forEach(logo => logo.classList.remove('auto-hovered'));
    });

    // Função que verifica continuamente quais logos estão sob o cursor
    function checkLogosUnderMouse() {
        if (mouseX === null || mouseY === null) return;

        logos.forEach(logo => {
            const rect = logo.getBoundingClientRect();
            
            // Verifica se o mouse está sobre a logo
            const isUnderMouse = (
                mouseX >= rect.left &&
                mouseX <= rect.right &&
                mouseY >= 0 &&
                mouseY <= marqueeContainer.clientHeight
            );

            if (isUnderMouse) {
                logo.classList.add('auto-hovered');
            } else {
                logo.classList.remove('auto-hovered');
            }
        });
    }

    // Executa a verificação a cada frame (60 vezes por segundo)
    function animate() {
        checkLogosUnderMouse();
        requestAnimationFrame(animate);
    }

    animate();
});