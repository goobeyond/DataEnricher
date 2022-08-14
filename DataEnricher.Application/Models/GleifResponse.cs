namespace DataEnricher.Application.Models
{

    public class GleifResponse
    {
        public Meta meta { get; set; }
        public Links links { get; set; }
        public Datum[] data { get; set; }
    }

    public class Meta
    {
        public Goldencopy goldenCopy { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Goldencopy
    {
        public DateTime publishDate { get; set; }
    }

    public class Pagination
    {
        public int currentPage { get; set; }
        public int perPage { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public int total { get; set; }
        public int lastPage { get; set; }
    }

    public class Links
    {
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
        public Links8 links { get; set; }
    }

    public class Attributes
    {
        public string lei { get; set; }
        public Entity entity { get; set; }
        public Registration registration { get; set; }
        public string[] bic { get; set; }
    }

    public class Entity
    {
        public Legalname legalName { get; set; }
        public Othername[] otherNames { get; set; }
        public object[] transliteratedOtherNames { get; set; }
        public Legaladdress legalAddress { get; set; }
        public Headquartersaddress headquartersAddress { get; set; }
        public Registeredat registeredAt { get; set; }
        public string registeredAs { get; set; }
        public string jurisdiction { get; set; }
        public string category { get; set; }
        public Legalform legalForm { get; set; }
        public Associatedentity associatedEntity { get; set; }
        public string status { get; set; }
        public Expiration expiration { get; set; }
        public Successorentity successorEntity { get; set; }
        public object[] successorEntities { get; set; }
        public DateTime? creationDate { get; set; }
        public object subCategory { get; set; }
        public object[] otherAddresses { get; set; }
        public object[] eventGroups { get; set; }
    }

    public class Legalname
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class Legaladdress
    {
        public string language { get; set; }
        public string[] addressLines { get; set; }
        public object addressNumber { get; set; }
        public object addressNumberWithinBuilding { get; set; }
        public object mailRouting { get; set; }
        public string city { get; set; }
        public object region { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
    }

    public class Headquartersaddress
    {
        public string language { get; set; }
        public string[] addressLines { get; set; }
        public object addressNumber { get; set; }
        public object addressNumberWithinBuilding { get; set; }
        public object mailRouting { get; set; }
        public string city { get; set; }
        public object region { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
    }

    public class Registeredat
    {
        public string id { get; set; }
        public object other { get; set; }
    }

    public class Legalform
    {
        public string id { get; set; }
        public object other { get; set; }
    }

    public class Associatedentity
    {
        public object lei { get; set; }
        public object name { get; set; }
    }

    public class Expiration
    {
        public object date { get; set; }
        public object reason { get; set; }
    }

    public class Successorentity
    {
        public object lei { get; set; }
        public object name { get; set; }
    }

    public class Othername
    {
        public string name { get; set; }
        public string language { get; set; }
        public string type { get; set; }
    }

    public class Registration
    {
        public DateTime initialRegistrationDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string status { get; set; }
        public DateTime nextRenewalDate { get; set; }
        public string managingLou { get; set; }
        public string corroborationLevel { get; set; }
        public Validatedat validatedAt { get; set; }
        public string validatedAs { get; set; }
        public object[] otherValidationAuthorities { get; set; }
    }

    public class Validatedat
    {
        public string id { get; set; }
        public object other { get; set; }
    }

    public class Relationships
    {
        public ManagingLou managinglou { get; set; }
        public LeiIssuer leiissuer { get; set; }
        public FieldModifications fieldmodifications { get; set; }
        public DirectParent directparent { get; set; }
        public UltimateParent ultimateparent { get; set; }
        public DirectChildren directchildren { get; set; }
        public Isins isins { get; set; }
    }

    public class ManagingLou
    {
        public Links1 links { get; set; }
    }

    public class Links1
    {
        public string related { get; set; }
    }

    public class LeiIssuer
    {
        public Links2 links { get; set; }
    }

    public class Links2
    {
        public string related { get; set; }
    }

    public class FieldModifications
    {
        public Links3 links { get; set; }
    }

    public class Links3
    {
        public string related { get; set; }
    }

    public class DirectParent
    {
        public Links4 links { get; set; }
    }

    public class Links4
    {
        public string relationshiprecord { get; set; }
        public string leirecord { get; set; }
    }

    public class UltimateParent
    {
        public Links5 links { get; set; }
    }

    public class Links5
    {
        public string relationshiprecord { get; set; }
        public string leirecord { get; set; }
    }

    public class DirectChildren
    {
        public Links6 links { get; set; }
    }

    public class Links6
    {
        public string relationshiprecords { get; set; }
        public string related { get; set; }
    }

    public class Isins
    {
        public Links7 links { get; set; }
    }

    public class Links7
    {
        public string related { get; set; }
    }

    public class Links8
    {
        public string self { get; set; }
    }

}