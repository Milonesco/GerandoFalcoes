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

    // --- FUNÇÃO DE SMOOTH SCROLL PERSONALIZADA (GARANTIDA) ---
    function smoothScrollTo(targetPosition, duration = 800) {
        const startPosition = window.pageYOffset;
        const distance = targetPosition - startPosition;
        let startTime = null;

        function animation(currentTime) {
            if (startTime === null) startTime = currentTime;
            const timeElapsed = currentTime - startTime;
            const run = ease(timeElapsed, startPosition, distance, duration);
            window.scrollTo(0, run);
            if (timeElapsed < duration) requestAnimationFrame(animation);
        }

        // Função de easing para suavizar a animação (ease-in-out)
        function ease(t, b, c, d) {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        }

        requestAnimationFrame(animation);
    }

    // --- SMOOTH SCROLL PARA OS LINKS DA NAVBAR ---
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link[href^="#"]');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            const targetId = this.getAttribute('href').substring(1);
            const targetSection = document.getElementById(targetId);
            
            if (targetSection) {
                const navbarHeight = navbar ? navbar.offsetHeight : 0;
                const additionalOffset = 30;
                const targetPosition = targetSection.offsetTop - navbarHeight - additionalOffset;
                
                // USA A FUNÇÃO PERSONALIZADA
                smoothScrollTo(targetPosition, 1000); // 1000ms = 1 segundo de duração
                
                // Fecha o menu mobile
                const navbarCollapse = document.querySelector('.navbar-collapse');
                if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                    const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse) || new bootstrap.Collapse(navbarCollapse, { toggle: false });
                    bsCollapse.hide();
                }
            }
        });
    });

    // --- SMOOTH SCROLL PARA O LOGO (VOLTA AO TOPO) ---
    const brandLink = document.querySelector('.navbar-brand[href="#inicio"]');
    if (brandLink) {
        brandLink.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            // USA A FUNÇÃO PERSONALIZADA PARA VOLTAR AO TOPO
            smoothScrollTo(0, 1000);
            
            // Fecha menu mobile se aberto
            const navbarCollapse = document.querySelector('.navbar-collapse');
            if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse) || new bootstrap.Collapse(navbarCollapse, { toggle: false });
                bsCollapse.hide();
            }
        });
    }

    // --- LÓGICA DO DARK MODE ---
    const toggleBtn = document.getElementById('darkModeToggle');
    const htmlElement = document.documentElement;
    const icon = toggleBtn ? toggleBtn.querySelector('i') : null;

    const savedTheme = localStorage.getItem('theme') || 'light';
    htmlElement.setAttribute('data-theme', savedTheme);
    updateIcon(savedTheme);

    if (toggleBtn) {
        toggleBtn.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';

            htmlElement.setAttribute('data-theme', newTheme);
            localStorage.setItem('theme', newTheme);
            updateIcon(newTheme);
        });
    }

    function updateIcon(theme) {
        if (!icon) return;

        if (theme === 'dark') {
            icon.classList.remove('bi-moon-fill');
            icon.classList.add('bi-sun-fill');
            icon.style.color = "#ffd700";
        } else {
            icon.classList.remove('bi-sun-fill');
            icon.classList.add('bi-moon-fill');
            icon.style.color = "";
        }
    }

    // --- AUTO-HOVER NOS CÍRCULOS DE LOGOS DO CARROSSEL ---
    const marqueeContainer = document.querySelector('.marquee-container');
    
    if (marqueeContainer) {
        const logoCircles = marqueeContainer.querySelectorAll('.logo-circle');
        let mouseX = null;
        let mouseY = null;

        marqueeContainer.addEventListener('mousemove', function (e) {
            const rect = marqueeContainer.getBoundingClientRect();
            mouseX = e.clientX;
            mouseY = e.clientY - rect.top;
        });

        marqueeContainer.addEventListener('mouseleave', function () {
            mouseX = null;
            mouseY = null;
            logoCircles.forEach(circle => circle.classList.remove('auto-hovered'));
        });

        function checkCirclesUnderMouse() {
            if (mouseX === null || mouseY === null) return;

            logoCircles.forEach(circle => {
                const rect = circle.getBoundingClientRect();
                
                const isUnderMouse = (
                    mouseX >= rect.left &&
                    mouseX <= rect.right &&
                    mouseY >= 0 &&
                    mouseY <= marqueeContainer.clientHeight
                );

                if (isUnderMouse) {
                    circle.classList.add('auto-hovered');
                } else {
                    circle.classList.remove('auto-hovered');
                }
            });
        }

        function animateLogos() {
            checkCirclesUnderMouse();
            requestAnimationFrame(animateLogos);
        }

        animateLogos();
    }

    // ========================================================================
    // === ANIMAÇÕES DO FORMULÁRIO DE INSCRIÇÃO ===
    // ========================================================================

    const inscriptionCard = document.querySelector('.inscription-card');
    
    if (inscriptionCard) {
        
        // --- 1. FUNÇÃO PARA ANIMAR TRANSIÇÃO ENTRE PASSOS ---
        function animateStepTransition(currentStepElement, nextStepElement, direction = 'forward') {
            if (!currentStepElement || !nextStepElement) return;

            // Adiciona animação de saída ao passo atual
            currentStepElement.classList.add('inscription-step-exit');
            
            // Aguarda a animação de saída terminar
            setTimeout(() => {
                currentStepElement.style.display = 'none';
                currentStepElement.classList.remove('inscription-step-exit');
                
                // Exibe e anima o próximo passo
                nextStepElement.style.display = 'block';
                nextStepElement.classList.add('inscription-step-enter');
                
                // Remove a classe após a animação
                setTimeout(() => {
                    nextStepElement.classList.remove('inscription-step-enter');
                }, 500);
            }, 300);
        }

        // --- 2. INTERCEPTAR CLIQUES NOS BOTÕES DE NAVEGAÇÃO ---
        const btnNext = document.querySelectorAll('.btn-dark, .btn-success'); // Botões "Próximo" e "Finalizar"
        const btnPrev = document.querySelectorAll('.btn-outline-secondary'); // Botões "Voltar"

        // Detecta qual step está visível atualmente
        function getCurrentStep() {
            const steps = document.querySelectorAll('[id^="step"]');
            for (let step of steps) {
                if (step.style.display !== 'none' && window.getComputedStyle(step).display !== 'none') {
                    return step;
                }
            }
            return null;
        }

        // Adiciona listeners aos botões "Próximo"
        btnNext.forEach(btn => {
            btn.addEventListener('click', function(e) {
                // Só anima se a validação passar (você pode adicionar lógica de validação aqui)
                const currentStep = getCurrentStep();
                
                // Adiciona loading ao card
                inscriptionCard.classList.add('inscription-card-loading', 'inscription-card-pulse');
                
                // Remove loading após a animação
                setTimeout(() => {
                    inscriptionCard.classList.remove('inscription-card-loading', 'inscription-card-pulse');
                }, 1500);
                
                // A transição de passo será controlada pelo seu código existente
                // Mas garantimos que a animação visual aconteça
            });
        });

        // Adiciona listeners aos botões "Voltar"
        btnPrev.forEach(btn => {
            btn.addEventListener('click', function(e) {
                const currentStep = getCurrentStep();
                
                // Adiciona shimmer ao voltar também
                inscriptionCard.classList.add('inscription-card-loading');
                
                setTimeout(() => {
                    inscriptionCard.classList.remove('inscription-card-loading');
                }, 1500);
            });
        });

        // --- 3. ANIMAÇÃO INICIAL NO CARREGAMENTO DA PÁGINA ---
        // Anima o card quando a página de inscrição carrega
        setTimeout(() => {
            inscriptionCard.classList.add('inscription-card-pulse');
            setTimeout(() => {
                inscriptionCard.classList.remove('inscription-card-pulse');
            }, 1500);
        }, 300);

        // --- 4. ANIMAÇÃO QUANDO CAMPOS SÃO PREENCHIDOS (OPCIONAL) ---
        const formInputs = document.querySelectorAll('.form-control, .form-select');
        
        formInputs.forEach(input => {
            input.addEventListener('focus', function() {
                this.style.transition = 'all 0.3s ease';
            });
            
            input.addEventListener('blur', function() {
                if (this.value !== '') {
                    // Adiciona uma pequena animação de sucesso quando o campo é preenchido
                    this.style.transform = 'scale(1.02)';
                    setTimeout(() => {
                        this.style.transform = 'scale(1)';
                    }, 200);
                }
            });
        });

        // --- 5. OBSERVADOR PARA MUDANÇAS NA BARRA DE PROGRESSO ---
        const progressBar = document.querySelector('.inscription-progress-bar');
        
        if (progressBar) {
            // Observa mudanças no estilo (quando a largura muda)
            const observer = new MutationObserver(function(mutations) {
                mutations.forEach(function(mutation) {
                    if (mutation.attributeName === 'style') {
                        // Adiciona pulse ao card quando o progresso muda
                        inscriptionCard.classList.add('inscription-card-pulse');
                        setTimeout(() => {
                            inscriptionCard.classList.remove('inscription-card-pulse');
                        }, 1500);
                    }
                });
            });
            
            observer.observe(progressBar, { attributes: true });
        }

        // --- 6. LOADING SPINNER AO SUBMETER O FORMULÁRIO ---
        const form = document.querySelector('form');
        
        if (form) {
            form.addEventListener('submit', function(e) {
                // Cria e adiciona o spinner
                const submitBtn = this.querySelector('.btn-success');
                if (submitBtn) {
                    const originalText = submitBtn.innerHTML;
                    submitBtn.disabled = true;
                    submitBtn.innerHTML = '<span class="inscription-loading-spinner me-2"></span>Enviando...';
                    
                    // Adiciona animação de loading ao card
                    inscriptionCard.classList.add('inscription-card-loading', 'inscription-card-pulse');
                }
            });
        }
    }
});