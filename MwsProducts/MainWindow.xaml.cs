using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;
using MarketplaceWebServiceProducts.Mock;
using System.Windows;
using System.Collections.Generic;

namespace MwsProducts
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get Service Status Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceStatusSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);

            GetServiceStatusRequest request = new GetServiceStatusRequest();
            request.SellerId = SellerId;
            request.MWSAuthToken = MWSAuthToken;

            GetServiceStatusResponse response = client.GetServiceStatus(request);
            if (response.IsSetGetServiceStatusResult())
            {
                txtServiceStatusResult.Text = "利用状況：" + response.GetServiceStatusResult.Status;
            }
        }

        /// <summary>
        /// Get ListMatching Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListMatchingSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);

            ListMatchingProductsRequest request = new ListMatchingProductsRequest();

            request.SellerId = SellerId;
            request.MWSAuthToken = MWSAuthToken;
            request.MarketplaceId = MarketplaceId;
            request.Query = txtListMatchingSearchValue.Text.ToString().Trim();

            ListMatchingProductsResponse response = client.ListMatchingProducts(request);
            if (response.IsSetListMatchingProductsResult())
            {
                ListMatchingProductsResult ListMatchingProductsResult = response.ListMatchingProductsResult;
                if (ListMatchingProductsResult.IsSetProducts())
                {
                    ProductList productList = ListMatchingProductsResult.Products;
                    List<Product> products = productList.Product;
                    foreach (Product product in products)
                    {
                        System.Xml.XmlElement elements = (System.Xml.XmlElement)product.AttributeSets.Any[0];

                        foreach (System.Xml.XmlElement element in elements)
                        {
                            switch (element.LocalName)
                            {
                                case "Title":
                                    strbuff += element.InnerText + System.Environment.NewLine;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    txtListMatchingResult.Text = strbuff;
                }
            }
        }

        /// <summary>
        /// Get Matching Product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatchingSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetMatchingProductRequest request = new GetMatchingProductRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;

            ASINListType asinListType = new ASINListType();
            asinListType.ASIN.Add(txtSearchValue.Text);
            request.ASINList = asinListType;
            request.MWSAuthToken = MWSAuthToken;

            GetMatchingProductResponse response = client.GetMatchingProduct(request);
            if (response.IsSetGetMatchingProductResult())
            {
                List<GetMatchingProductResult> getMatchingProductResultList = response.GetMatchingProductResult;
                foreach (GetMatchingProductResult getMatchingProductResult in getMatchingProductResultList)
                {
                    Product product = getMatchingProductResult.Product;
                    System.Xml.XmlElement elements = (System.Xml.XmlElement)product.AttributeSets.Any[0];

                    foreach (System.Xml.XmlElement element in elements)
                    {
                        switch (element.LocalName)
                        {
                            case "Title":
                                strbuff += "タイトル:" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "Creator":
                                strbuff += "著者：" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "ListPrice":
                                strbuff += "価格：" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "Manufacturer":
                                strbuff += "販売会社：" + element.InnerText + System.Environment.NewLine;
                                break;
                            default:
                                break;
                        }
                    }
                    txtMatchingResult.Text = strbuff;
                }
            }
        }

        /// <summary>
        /// Get Matching Product For ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatchingProductSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetMatchingProductForIdRequest request = new GetMatchingProductForIdRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.IdType = "ASIN";
            IdListType idList = new IdListType();
            idList.Id.Add(txtMatchingProductSearchValue.Text.ToString().Trim());
            request.IdList = idList;

            GetMatchingProductForIdResponse response = client.GetMatchingProductForId(request);
            if (response.IsSetGetMatchingProductForIdResult())
            {
                List<GetMatchingProductForIdResult> getMatchingProductForIdResultList = response.GetMatchingProductForIdResult;
                if (getMatchingProductForIdResultList[0].IsSetProducts())
                {
                    ProductList productList = getMatchingProductForIdResultList[0].Products;
                    List<Product> products = productList.Product;

                    System.Xml.XmlElement elements = (System.Xml.XmlElement)products[0].AttributeSets.Any[0];

                    foreach (System.Xml.XmlElement element in elements)
                    {
                        switch (element.LocalName)
                        {
                            case "Title":
                                strbuff += "タイトル:" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "Creator":
                                strbuff += "著者：" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "ListPrice":
                                strbuff += "価格：" + element.InnerText + System.Environment.NewLine;
                                break;
                            case "Manufacturer":
                                strbuff += "販売会社：" + element.InnerText + System.Environment.NewLine;
                                break;
                            default:
                                break;
                        }
                    }
                    txtMatchingProductResult.Text = strbuff;
                }
            }
        }

        /// <summary>
        /// Get Competitive Pricing For ASIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompetitivePricingSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetCompetitivePricingForASINRequest request = new GetCompetitivePricingForASINRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            ASINListType asinListType = new ASINListType();
            asinListType.ASIN.Add(txtSearchValue.Text);
            request.ASINList = asinListType;
            request.MWSAuthToken = MWSAuthToken;

            GetCompetitivePricingForASINResponse response = client.GetCompetitivePricingForASIN(request);
            if (response.IsSetGetCompetitivePricingForASINResult())
            {
                foreach (GetCompetitivePricingForASINResult getCompetitivePricingForASINResult in response.GetCompetitivePricingForASINResult)
                {
                    if (getCompetitivePricingForASINResult.IsSetProduct())
                    {
                        Product product = getCompetitivePricingForASINResult.Product;
                        if (product.IsSetCompetitivePricing())
                        {
                            CompetitivePricingType competitivePricing = product.CompetitivePricing;
                            if (competitivePricing.IsSetCompetitivePrices())
                            {
                                CompetitivePriceList competitivePrices = competitivePricing.CompetitivePrices;
                                foreach (CompetitivePriceType priceList in competitivePrices.CompetitivePrice)
                                {
                                    strbuff += "価格:" + priceList.Price.LandedPrice.Amount + System.Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                txtCompetitivePricingResult.Text = strbuff;
            }
        }

        /// <summary>
        /// Get Lowest Offer Listings For ASIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLowestOfferListingsSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetLowestOfferListingsForASINRequest request = new GetLowestOfferListingsForASINRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            ASINListType asinListType = new ASINListType();
            asinListType.ASIN.Add(txtLowestOfferListingsSearchValue.Text.ToString().Trim());
            request.ASINList = asinListType;

            GetLowestOfferListingsForASINResponse response = client.GetLowestOfferListingsForASIN(request);
            if (response.IsSetGetLowestOfferListingsForASINResult())
            {
                List<GetLowestOfferListingsForASINResult> getLowestOfferListingsForASINResultList = response.GetLowestOfferListingsForASINResult;
                foreach (GetLowestOfferListingsForASINResult getLowestOfferListingsForASINResult in getLowestOfferListingsForASINResultList)
                {
                    if (getLowestOfferListingsForASINResult.IsSetProduct())
                    {
                        Product product = getLowestOfferListingsForASINResult.Product;

                        if (product.IsSetLowestOfferListings())
                        {
                            LowestOfferListingList lowestOfferListingList = product.LowestOfferListings;
                            foreach (LowestOfferListingType lowestOfferListing in lowestOfferListingList.LowestOfferListing)
                            {
                                strbuff += "価格：" + lowestOfferListing.Price.ListingPrice.Amount + System.Environment.NewLine;
                            }
                        }
                    }
                }
                txtLowestOfferListingsResult.Text = strbuff;
            }
        }

        /// <summary>
        /// Get My Price For ASIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMyPriceSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetMyPriceForASINRequest request = new GetMyPriceForASINRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.MWSAuthToken = MWSAuthToken;
            ASINListType asinListType = new ASINListType();
            asinListType.ASIN.Add(txtMyPriceSearchValue.Text.ToString().Trim());
            request.ASINList = asinListType;

            GetMyPriceForASINResponse response = client.GetMyPriceForASIN(request);
            if (response.IsSetGetMyPriceForASINResult())
            {
                List<GetMyPriceForASINResult> getMyPriceForASINResult = response.GetMyPriceForASINResult;
                foreach (GetMyPriceForASINResult myPriceForASIN in getMyPriceForASINResult)
                {
                    Product product = myPriceForASIN.Product;

                    strbuff += "コンディション：" + product.Offers.Offer[0].ItemCondition + System.Environment.NewLine;
                    strbuff += "セラーID：" + product.Offers.Offer[0].SellerId + System.Environment.NewLine;
                    strbuff += "SKU：" + product.Offers.Offer[0].SellerSKU + System.Environment.NewLine;

                    txtMyPriceResult.Text = strbuff;
                }
            }
        }

        /// <summary>
        /// Get Product Category For ASIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductCategoriesSearch_Click(object sender, RoutedEventArgs e)
        {
            string SellerId = CommonValue.strMerchantId;
            string MarketplaceId = CommonValue.strMarketplaceId;
            string AccessKeyId = CommonValue.strAccessKeyId;
            string SecretKeyId = CommonValue.strSecretKeyId;
            string ApplicationVersion = CommonValue.strApplicationVersion;
            string ApplicationName = CommonValue.strApplicationName;
            string MWSAuthToken = CommonValue.strMWSAuthToken;
            string strbuff = string.Empty;

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = CommonValue.strServiceURL;

            MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             config);
            GetProductCategoriesForASINRequest request = new GetProductCategoriesForASINRequest();
            request.SellerId = SellerId;
            request.MarketplaceId = MarketplaceId;
            request.ASIN = txtProductCategoriesSearchValue.Text.ToString().Trim();
            GetProductCategoriesForASINResponse response = client.GetProductCategoriesForASIN(request);
            if (response.IsSetGetProductCategoriesForASINResult())
            {
                GetProductCategoriesForASINResult getProductCategoriesForASINResult = response.GetProductCategoriesForASINResult;
                List<Categories> selfList = getProductCategoriesForASINResult.Self;
                foreach (Categories self in selfList)
                {
                    strbuff += "カテゴリ名：" + self.ProductCategoryName + System.Environment.NewLine;
                }
                txtProductCategoriesResult.Text = strbuff;
            }
        }
    }
}
