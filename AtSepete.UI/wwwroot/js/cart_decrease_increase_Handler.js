// Decrease button click handler
function createDecreaseHandler(productId, productStock,marketId, quantityInput) {
    return function () {
        var quantity = parseInt(quantityInput.value);
        if (quantity > 1) {
            quantity--;
            quantityInput.value = quantity;
            updateCartItemQuantity(productId, marketId, quantity);
            
        } else {
            removeFromCart(productId, marketId);
        }
        renderCartItems();
    };
}
// Increase button click handler
function createIncreaseHandler(productId, productStock, marketId, quantityInput) {
    return function () {
        var quantity = parseInt(quantityInput.value);
        if (quantity < productStock) {
            quantity++;
            quantityInput.value = quantity;
            updateCartItemQuantity(productId, marketId, quantity);
        } else {
            // Sayfa yenilenerek sayı item.productStock miktarına eşitlensin
            location.reload();
        }
        renderCartItems();
    };
}


// Increase button click handler
//function createIncreaseHandler(productId,productStock, marketId, quantityInput) {
//    return function () {
//        var quantity = parseInt(quantityInput.value);
//        quantity++;
//        quantityInput.value = quantity;
//         updateCartItemQuantity(productId, marketId, quantity);
        
//        renderCartItems();
//    };
//}

function updateCartItemQuantity(productId, marketId, quantity)
{
    var cart = getCartFromLocalStorage();

    // Sepet ürünlerini dolaşarak ilgili ürünü bulun ve adetini güncelleyin
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].productId === productId && cart[i].marketId === marketId) {
            cart[i].quantity = quantity;
            break;
        }
    }

    // Güncellenmiş sepet verilerini localStorage'a kaydedin
    saveCartToLocalStorage(cart);
}

function saveCartToLocalStorage(cart) {

    var cartJson = JSON.stringify(cart);
    localStorage.setItem('Cart', cartJson);

}
