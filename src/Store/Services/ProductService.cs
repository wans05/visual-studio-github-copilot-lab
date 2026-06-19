using DataEntities;
using System.Text.Json;

namespace Store.Services;

public class ProductService
{
    HttpClient httpClient;
    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<List<Product>> GetProducts()
    {
        List<Product>? products = null;
        var response = await httpClient.GetAsync("/api/Product");
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            products = await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.ListProduct);
        }

        return products ?? new List<Product>();
    }

    public async Task<Product?> GetProductById(int id)
    {
        Product? product = null;
        var response = await httpClient.GetAsync($"/api/Product/{id}");
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            product = await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.Product);
        }
        return product;
    }

    public async Task<bool> CreateProduct(Product product)
    {
        var response = await httpClient.PostAsJsonAsync("/api/Product", product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProduct(int id, Product updatedProduct)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/Product/{id}", updatedProduct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/Product/{id}");
        return response.IsSuccessStatusCode;
    }
}