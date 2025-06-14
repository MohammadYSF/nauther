import type { BaseApiResponseModel } from "./baseApiResponseModel";
import type { Permission } from "./permission";

interface Role {
    id: string,
    name: string,
    displayName: string,
    permissions: Permission[],
    users: { id: string, name: string }[],
}
interface CreateRolePayload {
    name: string,
    displayName: string,
    permissions: string[]
};
interface EditRolePayload extends CreateRolePayload {
    id: string
};

interface GetRoleByIdResponseDataModel extends BaseApiResponseModel<Role> {

};
interface GetRolesResponseDataModel extends BaseApiResponseModel<Role[]> {
    metadata: { total: number }
};

interface DeleteRoleResponseDataModel extends BaseApiResponseModel<{ ids: string[] }> {
};

interface CreateRoleResponseModel extends BaseApiResponseModel<{id:string}> {
};
export type {CreateRoleResponseModel,DeleteRoleResponseDataModel,
    GetRolesResponseDataModel,GetRoleByIdResponseDataModel,
    EditRolePayload,CreateRolePayload,Role
}