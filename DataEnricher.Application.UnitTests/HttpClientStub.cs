using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataEnricher.Application.UnitTests
{
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        private string bic;
        private string legalName;
        private string legalCountry;

        public HttpMessageHandlerStub(string bic, string legalName, string legalCountry)
        {
            this.bic = bic;
            this.legalName = legalName;
            this.legalCountry = legalCountry;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(GetGleifJson(bic, legalCountry, legalName), Encoding.UTF8, "application/json"),
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return Task.FromResult(response);
        }

        public string GetGleifJson(string bic, string legalCountry, string legalName) => 
                                @$"{{
                                      ""meta"": {{
                                        ""goldenCopy"": {{
                                          ""publishDate"": ""2022-08-13T00:00:00Z""
                                        }},
                                        ""pagination"": {{
                                          ""currentPage"": 1,
                                          ""perPage"": 10,
                                          ""from"": 1,
                                          ""to"": 1,
                                          ""total"": 1,
                                          ""lastPage"": 1
                                        }}
                                      }},
                                      ""links"": {{
                                        ""first"": ""https://api.gleif.org/api/v1/lei-records?filter%5Blei%5D=XKZZ2JZF41MRHTR1V493&page%5Bnumber%5D=1&page%5Bsize%5D=10"",
                                        ""last"": ""https://api.gleif.org/api/v1/lei-records?filter%5Blei%5D=XKZZ2JZF41MRHTR1V493&page%5Bnumber%5D=1&page%5Bsize%5D=10""
                                      }},
                                      ""data"": [
                                        {{
                                          ""type"": ""lei-records"",
                                          ""id"": ""XKZZ2JZF41MRHTR1V493"",
                                          ""attributes"": {{
                                            ""lei"": ""XKZZ2JZF41MRHTR1V493"",
                                            ""entity"": {{
                                              ""legalName"": {{
                                                ""name"": ""{legalName}"",
                                                ""language"": ""en""
                                              }},
                                              ""otherNames"": [
                                                {{
                                                  ""name"": ""STOCKROBE LIMITED"",
                                                  ""language"": ""en"",
                                                  ""type"": ""PREVIOUS_LEGAL_NAME""
                                                }}
                                              ],
                                              ""transliteratedOtherNames"": [],
                                              ""legalAddress"": {{
                                                  ""language"": ""en"",
                                                  ""addressLines"": [
                                                  ""Citigroup Centre"",
                                                  ""Canada Square"",
                                                  ""Canary Wharf""
                                                ],
                                                ""addressNumber"": null,
                                                ""addressNumberWithinBuilding"": null,
                                                ""mailRouting"": null,
                                                ""city"": ""London"",
                                                ""region"": null,
                                                ""country"": ""{legalCountry}"",
                                                ""postalCode"": ""E14 5LB""
                                              }},
                                              ""headquartersAddress"": {{
                                        ""language"": ""en"",
                                                ""addressLines"": [
                                                  ""Citigroup Centre"",
                                                  ""Canada Square"",
                                                  ""Canary Wharf""
                                                ],
                                                ""addressNumber"": null,
                                                ""addressNumberWithinBuilding"": null,
                                                ""mailRouting"": null,
                                                ""city"": ""London"",
                                                ""region"": null,
                                                ""country"": ""GB"",
                                                ""postalCode"": ""E14 5LB""
                                              }},
                                              ""registeredAt"": {{
                                        ""id"": ""RA000585"",
                                                ""other"": null
                                              }},
                                              ""registeredAs"": ""01763297"",
                                              ""jurisdiction"": ""GB"",
                                              ""category"": ""GENERAL"",
                                              ""legalForm"": {{
                                        ""id"": ""H0PO"",
                                                ""other"": null
                                              }},
                                              ""associatedEntity"": {{
                                        ""lei"": null,
                                                ""name"": null
                                              }},
                                              ""status"": ""ACTIVE"",
                                              ""expiration"": {{
                                        ""date"": null,
                                                ""reason"": null
                                              }},
                                              ""successorEntity"": {{
                                        ""lei"": null,
                                                ""name"": null
                                              }},
                                              ""successorEntities"": [],
                                              ""creationDate"": ""1983-10-21T00:00:00Z"",
                                              ""subCategory"": null,
                                              ""otherAddresses"": [],
                                              ""eventGroups"": []
                                            }},
                                            ""registration"": {{
                                        ""initialRegistrationDate"": ""2012-06-06T15:55:00Z"",
                                              ""lastUpdateDate"": ""2022-04-30T14:23:34Z"",
                                              ""status"": ""ISSUED"",
                                              ""nextRenewalDate"": ""2023-05-27T14:17:44Z"",
                                              ""managingLou"": ""529900T8BM49AURSDO55"",
                                              ""corroborationLevel"": ""FULLY_CORROBORATED"",
                                              ""validatedAt"": {{
                                            ""id"": ""RA000585"",
                                                ""other"": null
                                              }},
                                              ""validatedAs"": ""01763297"",
                                              ""otherValidationAuthorities"": []
                                            }},
                                            ""bic"": [
                                              ""{bic}""
                                            ]
                                          }},
                                          ""relationships"": {{
                                        ""managing-lou"": {{
                                            ""links"": {{
                                                ""related"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/managing-lou""
                                              }}
                                        }},
                                            ""lei-issuer"": {{
                                            ""links"": {{
                                                ""related"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/lei-issuer""
                                              }}
                                        }},
                                            ""field-modifications"": {{
                                            ""links"": {{
                                                ""related"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/field-modifications""
                                              }}
                                        }},
                                            ""direct-parent"": {{
                                            ""links"": {{
                                                ""relationship-record"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/direct-parent-relationship"",
                                                ""lei-record"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/direct-parent""
                                              }}
                                        }},
                                            ""ultimate-parent"": {{
                                            ""links"": {{
                                                ""relationship-record"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/ultimate-parent-relationship"",
                                                ""lei-record"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/ultimate-parent""
                                              }}
                                        }},
                                            ""direct-children"": {{
                                            ""links"": {{
                                                ""relationship-records"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/direct-child-relationships"",
                                                ""related"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/direct-children""
                                              }}
                                        }},
                                            ""isins"": {{
                                            ""links"": {{
                                                ""related"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493/isins""
                                              }}
                                        }}
                                    }},
                                          ""links"": {{
                                        ""self"": ""https://api.gleif.org/api/v1/lei-records/XKZZ2JZF41MRHTR1V493""
                                          }}
                                        }}
                                      ]
                                    }}";
    }
}
