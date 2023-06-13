//SEPETE ATILAN ÜRÜN SAYISININ İCON'UNDA GÜNCELLENMESİ

 // Bu kod, yerel depolamadan sepet verilerini almak için kullanılan bir fonksiyonduR

function getCartFromLocalStorage() {
    var cartJson = localStorage.getItem('Cart');
    var cart = [];

    if (cartJson) {
        try {
            cart = JSON.parse(cartJson);
        } catch (error) {
            console.error('Invalid JSON format in cart data:', error);
        }
    }

    document.getElementById('cartItemCount').textContent = cart.length;
    return cart;
}
//Bu kod, sayfanın yüklenmesini bekler ve yükleme tamamlandığında updateCartItemCount fonksiyonunu çağırır. Böylece, sayfa içeriği tamamen yüklendiğinde otomatik olarak updateCartItemCount fonksiyonu çalışır.
window.addEventListener('DOMContentLoaded', function () {
    updateCartItemCount();
});

function updateCartItemCount() {
    //bu fonksiyon sebet verilerini getirir ve getirdiği json dizisini saydırır ve sepetteki sayıyı bu şekilde alır
    var cart = getCartFromLocalStorage();
    var cartItemCount = cart.length;
}