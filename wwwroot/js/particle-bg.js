// ============================================
// FitGain — Particle Background Effect
// Vanilla JS Canvas-based particle text animation
// ============================================

(function () {
    'use strict';

    // --- Particle Class ---
    class Particle {
        constructor() {
            this.pos = { x: 0, y: 0 };
            this.vel = { x: 0, y: 0 };
            this.acc = { x: 0, y: 0 };
            this.target = { x: 0, y: 0 };
            this.closeEnoughTarget = 100;
            this.maxSpeed = 1.0;
            this.maxForce = 0.1;
            this.particleSize = 3;
            this.isKilled = false;
            this.startColor = { r: 249, g: 115, b: 22 };
            this.targetColor = { r: 249, g: 115, b: 22 };
            this.colorWeight = 0;
            this.colorBlendRate = 0.01;
        }

        move() {
            let proximityMult = 1;
            const dx = this.pos.x - this.target.x;
            const dy = this.pos.y - this.target.y;
            const distance = Math.sqrt(dx * dx + dy * dy);

            if (distance < this.closeEnoughTarget) {
                proximityMult = distance / this.closeEnoughTarget;
            }

            const toTarget = {
                x: this.target.x - this.pos.x,
                y: this.target.y - this.pos.y
            };

            const mag = Math.sqrt(toTarget.x * toTarget.x + toTarget.y * toTarget.y);
            if (mag > 0) {
                toTarget.x = (toTarget.x / mag) * this.maxSpeed * proximityMult;
                toTarget.y = (toTarget.y / mag) * this.maxSpeed * proximityMult;
            }

            const steer = {
                x: toTarget.x - this.vel.x,
                y: toTarget.y - this.vel.y
            };

            const steerMag = Math.sqrt(steer.x * steer.x + steer.y * steer.y);
            if (steerMag > 0) {
                steer.x = (steer.x / steerMag) * this.maxForce;
                steer.y = (steer.y / steerMag) * this.maxForce;
            }

            this.acc.x += steer.x;
            this.acc.y += steer.y;
            this.vel.x += this.acc.x;
            this.vel.y += this.acc.y;
            this.pos.x += this.vel.x;
            this.pos.y += this.vel.y;
            this.acc.x = 0;
            this.acc.y = 0;
        }

        draw(ctx) {
            if (this.colorWeight < 1.0) {
                this.colorWeight = Math.min(this.colorWeight + this.colorBlendRate, 1.0);
            }

            const r = Math.round(this.startColor.r + (this.targetColor.r - this.startColor.r) * this.colorWeight);
            const g = Math.round(this.startColor.g + (this.targetColor.g - this.startColor.g) * this.colorWeight);
            const b = Math.round(this.startColor.b + (this.targetColor.b - this.startColor.b) * this.colorWeight);

            ctx.fillStyle = `rgb(${r}, ${g}, ${b})`;
            ctx.fillRect(this.pos.x, this.pos.y, 2, 2);
        }

        kill(width, height) {
            if (!this.isKilled) {
                const rPos = generateRandomPos(width / 2, height / 2, (width + height) / 2);
                this.target.x = rPos.x;
                this.target.y = rPos.y;

                this.startColor = {
                    r: this.startColor.r + (this.targetColor.r - this.startColor.r) * this.colorWeight,
                    g: this.startColor.g + (this.targetColor.g - this.startColor.g) * this.colorWeight,
                    b: this.startColor.b + (this.targetColor.b - this.startColor.b) * this.colorWeight
                };
                this.targetColor = { r: 0, g: 0, b: 0 };
                this.colorWeight = 0;
                this.isKilled = true;
            }
        }
    }

    function generateRandomPos(x, y, mag) {
        const rx = Math.random() * 1000;
        const ry = Math.random() * 500;
        const dir = { x: rx - x, y: ry - y };
        const m = Math.sqrt(dir.x * dir.x + dir.y * dir.y);
        if (m > 0) {
            dir.x = (dir.x / m) * mag;
            dir.y = (dir.y / m) * mag;
        }
        return { x: x + dir.x, y: y + dir.y };
    }

    // --- Theme-aligned color palettes ---
    const COLORS = [
        { r: 249, g: 115, b: 22 },    // orange
        { r: 220, g: 38,  b: 38 },    // red
        { r: 251, g: 146, b: 60 },    // light orange
        { r: 239, g: 68,  b: 68 },    // light red
        { r: 253, g: 186, b: 116 },   // bright orange
        { r: 234, g: 88,  b: 12 },    // deep orange
    ];

    const WORDS = ['FITGAIN', 'STRENGTH', 'POWER', 'TRAIN', 'PROGRESS', 'GRIND'];

    // --- Main Controller ---
    function initParticleBg() {
        const canvas = document.getElementById('particleBgCanvas');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        let particles = [];
        let frameCount = 0;
        let wordIndex = 0;
        const pixelSteps = 5;
        let animId;

        function resize() {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
            // Re-render current word on resize
            nextWord(WORDS[wordIndex]);
        }

        function nextWord(word) {
            const offscreen = document.createElement('canvas');
            offscreen.width = canvas.width;
            offscreen.height = canvas.height;
            const offCtx = offscreen.getContext('2d');

            // Scale font to canvas size
            const fontSize = Math.min(canvas.width / (word.length * 0.65), canvas.height * 0.35);
            offCtx.fillStyle = 'white';
            offCtx.font = `900 ${fontSize}px "Outfit", Arial, sans-serif`;
            offCtx.textAlign = 'center';
            offCtx.textBaseline = 'middle';
            offCtx.fillText(word, canvas.width / 2, canvas.height / 2);

            const imageData = offCtx.getImageData(0, 0, canvas.width, canvas.height);
            const pixels = imageData.data;

            const newColor = COLORS[Math.floor(Math.random() * COLORS.length)];

            let particleIndex = 0;
            const coordsIndexes = [];

            for (let i = 0; i < pixels.length; i += pixelSteps * 4) {
                coordsIndexes.push(i);
            }

            // Shuffle
            for (let i = coordsIndexes.length - 1; i > 0; i--) {
                const j = Math.floor(Math.random() * (i + 1));
                [coordsIndexes[i], coordsIndexes[j]] = [coordsIndexes[j], coordsIndexes[i]];
            }

            for (const ci of coordsIndexes) {
                const alpha = pixels[ci + 3];
                if (alpha > 0) {
                    const x = (ci / 4) % canvas.width;
                    const y = Math.floor(ci / 4 / canvas.width);

                    let particle;
                    if (particleIndex < particles.length) {
                        particle = particles[particleIndex];
                        particle.isKilled = false;
                        particleIndex++;
                    } else {
                        particle = new Particle();
                        const rp = generateRandomPos(canvas.width / 2, canvas.height / 2, (canvas.width + canvas.height) / 2);
                        particle.pos.x = rp.x;
                        particle.pos.y = rp.y;
                        particle.maxSpeed = Math.random() * 4 + 2;
                        particle.maxForce = particle.maxSpeed * 0.04;
                        particle.particleSize = Math.random() * 4 + 2;
                        particle.colorBlendRate = Math.random() * 0.02 + 0.002;
                        particles.push(particle);
                    }

                    particle.startColor = {
                        r: particle.startColor.r + (particle.targetColor.r - particle.startColor.r) * particle.colorWeight,
                        g: particle.startColor.g + (particle.targetColor.g - particle.startColor.g) * particle.colorWeight,
                        b: particle.startColor.b + (particle.targetColor.b - particle.startColor.b) * particle.colorWeight
                    };
                    particle.targetColor = newColor;
                    particle.colorWeight = 0;
                    particle.target.x = x;
                    particle.target.y = y;
                }
            }

            for (let i = particleIndex; i < particles.length; i++) {
                particles[i].kill(canvas.width, canvas.height);
            }
        }

        function animate() {
            // Semi-transparent clear for motion trails
            ctx.fillStyle = 'rgba(9, 9, 9, 0.12)';
            ctx.fillRect(0, 0, canvas.width, canvas.height);

            for (let i = particles.length - 1; i >= 0; i--) {
                const p = particles[i];
                p.move();
                p.draw(ctx);

                if (p.isKilled) {
                    if (p.pos.x < -50 || p.pos.x > canvas.width + 50 ||
                        p.pos.y < -50 || p.pos.y > canvas.height + 50) {
                        particles.splice(i, 1);
                    }
                }
            }

            frameCount++;
            if (frameCount % 300 === 0) {
                wordIndex = (wordIndex + 1) % WORDS.length;
                nextWord(WORDS[wordIndex]);
            }

            animId = requestAnimationFrame(animate);
        }

        // Initialize
        resize();
        window.addEventListener('resize', function () {
            clearTimeout(window._particleResizeTimer);
            window._particleResizeTimer = setTimeout(resize, 200);
        });

        animate();

        // Cleanup on page leave
        window.addEventListener('beforeunload', function () {
            cancelAnimationFrame(animId);
        });
    }

    // Start when DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initParticleBg);
    } else {
        initParticleBg();
    }
})();
