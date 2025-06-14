import type { BaseApiResponseModel } from "./baseApiResponseModel";
import type { Permission } from "./permission";
import type { Role } from "./role";

interface User {
    id: string;
    userCode: string;
    profileImage: string;
    username: string;
    phoneNumber: string;
    isActive: boolean;
    permissions:Permission[],
    roles:Role[]
};

interface GetAllUsersResponseDataModel_Raw extends BaseApiResponseModel<User[]> {
    metadata: { total: number }
}
interface GetAllUsersResponseDataModel extends BaseApiResponseModel<User[]> {
    metadata: { total: number }
}
export type {User,GetAllUsersResponseDataModel_Raw,GetAllUsersResponseDataModel}
