import type { BaseApiResponseModel } from "./baseApiResponseModel";
import type { Permission } from "./permission";
import type { Role } from "./role";
interface Admin {
    id: string,
    name: string,
    roles: Role[],
    permissions: Permission[]
}
interface GetAdminByIdResponseDataModel extends BaseApiResponseModel<Admin> {
};
type CreateAdminPayload = {
    name: string,
    roles: string[],
    permissions: string[],
    password: string,
    confirmPassword: string
}
type DeleteAdminPayload = {
    ids: string[]
}
interface DeleteAdminResponseDataModel extends BaseApiResponseModel<{ids:string[]}>  {

}
type EditAdminPayload = {
    name: string,
    roles: string[],
    permissions: string[],
    password?: string,
    confirmPassword?: string
}
interface CreateAdminResponseDataModel extends BaseApiResponseModel<{ids:string}>{

};
interface EditAdminResponseDataModel extends BaseApiResponseModel<{ids:string}>{

};

interface GetAdminsResponseDataModel extends BaseApiResponseModel<Admin[]>{
    metadata: { total: number }

};
export type {GetAdminByIdResponseDataModel,CreateAdminPayload,DeleteAdminPayload,
    DeleteAdminResponseDataModel,EditAdminPayload,CreateAdminResponseDataModel,
    EditAdminResponseDataModel,GetAdminsResponseDataModel
}