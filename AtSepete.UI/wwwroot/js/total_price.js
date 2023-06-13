function render() {
    var cart = getCartFromLocalStorage();
    var tableBody = document.querySelector('.shoping__cart__table tbody');

    // Tabloyu temizle
    tableBody.innerHTML = '';

    // Sepet ürünlerini dolaşarak tabloya ekleyin
    for (var i = 0; i < cart.length; i++) {
        var item = cart[i];

        // Ürün satırını oluşturun
        var row = document.createElement('tr');

        // ... Diğer sütunlar

        // Toplam sütunu
        var totalCell = document.createElement('td');
        totalCell.className = 'shoping__cart__total';
        row.appendChild(totalCell);

        var quantityInput = document.getElementById('quantityInput' + item.productId + item.marketId);
        updateTotal(item, quantityInput, totalCell); // Toplam tutarı güncellemek için fonksiyonu çağır

        // ... Diğer sütunlar

        // Satırı tabloya ekle
        tableBody.appendChild(row);
    }

    updateCheckoutTotals(); // Ödeme tutarlarını güncelle

    // Toplam tutarı güncellemek için fonksiyon tanımlama
    function updateTotal(item, quantityInput, totalCell) {
        quantityInput.addEventListener('input', function () {
            var total = item.productPrice * parseInt(quantityInput.value);
            totalCell.innerText = total + ' TL';

            updateCheckoutTotals();
        });
    }

    // Ödeme tutarlarını güncellemek için fonksiyon tanımlama
    function updateCheckoutTotals() {
        var alisverisTutariSpan = document.querySelector('.shoping__checkout li:first-child span');
        var odemeTutariSpan = document.querySelector('.shoping__checkout li:nth-child(2) span');

        var toplamAlisverisTutari = calculateTotal(); // Toplam tutarı yeniden hesapla
        console.log(toplamAlisverisTutari)
        alisverisTutariSpan.innerText = toplamAlisverisTutari + ' TL';
        odemeTutariSpan.innerText = toplamAlisverisTutari + ' TL';
    }

    // Toplam tutarı yeniden hesaplamak için fonksiyon tanımlama
    function calculateTotal() {
        var total = 0;
        for (var i = 0; i < cart.length; i++) {
            var item = cart[i];
            var quantityInput = document.getElementById('quantityInput' + item.productId + item.marketId);
            total += item.productPrice * parseInt(quantityInput.value);
        }
        return total;
    }
}

// Sayfa yüklendiğinde sepet ürünlerini render et
window.addEventListener('DOMContentLoaded', function () {
    render();
});

