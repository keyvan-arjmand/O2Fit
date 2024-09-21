using Identity.V2.Domain.Aggregates.PermissionCategoryAggregate;
using Mongo.Migration.Migrations.Document;

namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M004_MakeCategoryPermissionsIsDeleteToCamelCaseMigration: DocumentMigration<CategoryPermission>
{
    public M004_MakeCategoryPermissionsIsDeleteToCamelCaseMigration(string version) : base("0.0.4")
    {
    }

    public override void Up(BsonDocument document)
    {
        var serverCategoryPermissionIsDelete = document["IsDelete"].AsBoolean;
        var permissionsArray = document["permissions"].AsBsonArray;
        foreach (var bsonObject in permissionsArray)
        {
            var doc = bsonObject.AsBsonDocument;
            var pascalCaseIsDelete = doc["IsDelete"].AsBoolean;
            doc.Add("isDelete", pascalCaseIsDelete);
            doc.Remove("IsDelete");
        }
        
        document.Add("isDelete", serverCategoryPermissionIsDelete);
        document.Remove("IsDelete");
    }

    public override void Down(BsonDocument document)
    {
        var serverCategoryPermissionIsDelete = document["isDelete"].AsBoolean;
        var permissionsArray = document["permissions"].AsBsonArray;
        foreach (var bsonObject in permissionsArray)
        {
            var doc = bsonObject.AsBsonDocument;
            var pascalCaseIsDelete = doc["isDelete"].AsBoolean;
            doc.Add("IsDelete", pascalCaseIsDelete);
            doc.Remove("isDelete");
        }

        document.Add("IsDelete", serverCategoryPermissionIsDelete);
        document.Remove("isDelete");
    }
}