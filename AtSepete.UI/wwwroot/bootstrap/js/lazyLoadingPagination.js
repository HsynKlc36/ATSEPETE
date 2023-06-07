//window.addEventListener('scroll', function () {
//    // Sayfanın altına ulaşılıp ulaşılmadığını kontrol edin
//    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
//        // Yeni ürünleri yükleyin
//        loadMoreProducts();
//    }
//});

//function loadMoreProducts() {
//    // Yeni ürünleri yüklemek için AJAX veya başka bir yöntem kullanabilirsiniz
//    // AJAX örneği:
//    var startIndex = document.getElementsByClassName('product__item').length;
//    var endIndex = startIndex + 6;

//    // shopList'ten yeni ürünleri alın
//    var productsToShow = shopListPagination.slice(startIndex, endIndex);

//    // Ürünleri HTML'e dönüştürün ve sayfaya ekleyin
//    var productContainer = document.getElementById('product-container');

//    productsToShow.forEach(function (item) {
//        var divCol = document.createElement('div');
//        divCol.className = 'col-lg-4 col-md-6 col-sm-6';

//        var divProductItem = document.createElement('div');
//        divProductItem.className = 'product__item';

//        // Ürün içeriğini oluşturun...

//        divCol.appendChild(divProductItem);
//        productContainer.appendChild(divCol);
//    });
//}


