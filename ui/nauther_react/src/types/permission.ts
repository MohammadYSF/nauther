import type { BaseApiResponseModel } from "./baseApiResponseModel"

interface Permission {
    id: string,
    name: string,
    displayName: string
}
interface CreatePermissionPayload {
  name: string,
  displayName: string
};
interface EditPermissionPayload extends CreatePermissionPayload {
    id:string
};
interface DeletePermissionResponseDataModel extends BaseApiResponseModel<{ids:string[]}>{
}
interface CreatePermissionResponseDataModel extends BaseApiResponseModel<{id:string}>{
}
interface EditPermissionResponseDataModel extends BaseApiResponseModel<{id:string}>{
}
interface GetPermissionsResponseDataModel extends BaseApiResponseModel<Permission[]>{
  metadata: { total: number }
}
interface GetPermissionByIdResponseDataModel extends BaseApiResponseModel<Permission[]>{
  metadata: { total: number }
}

export type { Permission,CreatePermissionPayload,EditPermissionPayload,
    DeletePermissionResponseDataModel,CreatePermissionResponseDataModel,
    EditPermissionResponseDataModel,GetPermissionsResponseDataModel,
    GetPermissionByIdResponseDataModel
 }
