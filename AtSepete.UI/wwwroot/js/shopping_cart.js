
function getCartFromLocalStorage()// yerel depolamadan sepet verilerini almak için kullanılan bir fonksiyondur.
{
    var cartJson = localStorage.getItem('Cart');// ifadesiyle, 'Cart' adlı anahtarla yerel depolamada kaydedilmiş olan sepet verilerini alır.Bu veriler Json tipinde metinsel dizidir(string)
    return cartJson ? JSON.parse(cartJson) : [];//Ardından, cartJson değişkeni değeri kontrol edilir. Eğer cartJson değişkeni boş değilse veya hatalı bir JSON verisi içermiyorsa, yani yerel depolamadan alınan veri geçerli bir JSON dizi verisi ise, JSON.parse(cartJson) ile bu veriler JavaScript nesnesine dönüştürülür.fakat hatalıysa boş bir dizi döndürür
}
function saveCartToLocalStorage(cart) {
    var cartJson = JSON.stringify(cart);//JavaScript nesnesini JSON formatındaki bir dizeye dönüştürür.javaScript nesnelerini JSON formatına dönüştürdükten sonra JSON dizesi olarak yerel depolama alanına kaydeder
    localStorage.setItem('Cart', cartJson);//'Cart' adlı anahtarla cartJson değişkenindeki JSON dizesini yerel depolamada saklar. Bu işlem, sepet verilerini tarayıcının yerel depolama alanına kaydeder.Bu yöntem, yerel depolama alanına bir çift anahtar-değer çifti eklemek veya güncellemek için kullanılır.
}
function updateCartItemCount() {
    //bu fonksiyon sebet verilerini getirir ve getirdiği json dizisini saydırır ve sepetteki sayıyı bu şekilde alır
    var cart = getCartFromLocalStorage();
    var cartItemCount = cart.length;
}
function addToCartHomePage(productId, marketId, productTitle, productName, productQuantity, productUnit, marketName, productPrice, productPhotoPath) {

    var quantity = 1; // Varsayılan olarak eklenen ürünün miktarı 1

    // Local Storage'dan sepet verilerini al(js nesnesi olarak)
    var cart = getCartFromLocalStorage();

    // Ürünü sepete ekle
    var existingItem = cart.find(item => item.productId === productId && item.marketId === marketId);
    if (existingItem) {
        existingItem.quantity += quantity;
    }
    else {
        cart.push({

            productId: productId,
            marketId: marketId,
            productTitle: productTitle,
            productName: productName,
            productQuantity: productQuantity,
            productUnit: productUnit,
            productPhotoPath: productPhotoPath,
            marketName: marketName,
            productPrice: productPrice,
            quantity: quantity
        });
    }
    //güncellenen  Sepet verilerini Local Storage'a kaydet
    saveCartToLocalStorage(cart);
    //fonksiyonu çağrılarak, sepet öğe sayısı  güncellenir.
    updateCartItemCount();
}
function addToCartProductDetails(productId, marketId, productTitle, productName, productQuantity, productUnit, marketName, productPrice, productPhotoPath) {

    var quantity = 1; // Varsayılan olarak eklenen ürünün miktarı 1

    // Local Storage'dan sepet verilerini al(js nesnesi olarak)
    var cart = getCartFromLocalStorage();

    // Ürünü sepete ekle
    var existingItem = cart.find(item => item.productId === productId && item.marketId === marketId);
    if (existingItem) {
        existingItem.quantity += quantity;
    }
    else {
        cart.push({

            productId: productId,
            marketId: marketId,
            productTitle: productTitle,
            productName: productName,
            productQuantity: productQuantity,
            productUnit: productUnit,
            productPhotoPath: productPhotoPath,
            marketName: marketName,
            productPrice: productPrice,
            quantity: quantity
        });
    }
    //güncellenen  Sepet verilerini Local Storage'a kaydet
    saveCartToLocalStorage(cart);
    //fonksiyonu çağrılarak, sepet öğe sayısı  güncellenir.
    updateCartItemCount();
    location.reload();
}





