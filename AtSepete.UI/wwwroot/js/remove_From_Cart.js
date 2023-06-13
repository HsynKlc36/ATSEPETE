function removeFromCart(productId, marketId) { // Local Storage'dan sepet verilerini al
    var cart = getCartFromLocalStorage();
    // Ürünü sepetten çıkar 
    var index = cart.findIndex(item => item.productId === productId && item.marketId === marketId);
    if (index !== -1) {
        cart.splice(index, 1);
    }
    // Sepet verilerini Local Storage'a kaydet
    saveCartToLocalStorage(cart);
    // Sepet ürün sayısını güncelle
    updateCartItemCount();
}
function createRemoveHandler(productId, marketId) {
    return function () {
        removeFromCart(productId, marketId);
        renderCartItems()
    }
};



