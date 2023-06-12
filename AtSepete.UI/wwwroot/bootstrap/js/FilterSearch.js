function filterResults() {
    // Seçilen verileri al
    const selectedCategories = Array.from(document.querySelectorAll('#checkCategoryFilter:checked')).map(checkbox => checkbox.id.replace('checkCategoryFilter-', ''));
    const selectedBrands = Array.from(document.querySelectorAll('#checkTitleFilter:checked')).map(checkbox => checkbox.id.replace('checkTitleFilter-', ''));
    const selectedMarkets = Array.from(document.querySelectorAll('#checkMarketFilter:checked')).map(checkbox => checkbox.id.replace('checkMarketFilter-', ''));
    const minPrice = document.querySelector('#minamount').value;
    const maxPrice = document.querySelector('#maxamount').value;

    // Form elementini seç
    const form = document.getElementById('myForm');

    // Seçilen verileri gizli input alanlarına ekleyin
    const categoriesInput = document.createElement('input');
    categoriesInput.type = 'hidden';
    categoriesInput.name = 'selectedCategories';
    categoriesInput.value = JSON.stringify(selectedCategories);
    form.appendChild(categoriesInput);

    const brandsInput = document.createElement('input');
    brandsInput.type = 'hidden';
    brandsInput.name = 'selectedBrands';
    brandsInput.value = JSON.stringify(selectedBrands);
    form.appendChild(brandsInput);

    const marketsInput = document.createElement('input');
    marketsInput.type = 'hidden';
    marketsInput.name = 'selectedMarkets';
    marketsInput.value = JSON.stringify(selectedMarkets);
    form.appendChild(marketsInput);

    const minPriceInput = document.createElement('input');
    minPriceInput.type = 'hidden';
    minPriceInput.name = 'minPrice';
    minPriceInput.value = minPrice;
    form.appendChild(minPriceInput);

    const maxPriceInput = document.createElement('input');
    maxPriceInput.type = 'hidden';
    maxPriceInput.name = 'maxPrice';
    maxPriceInput.value = maxPrice;
    form.appendChild(maxPriceInput);

    // Formu gönder
    form.submit();
}