import api from './api';
type GetRoleByIdResponseDataModel = {
  id: string,
  name: string,
  displayName: string,
  permissions: { id: string, name: string,displayName:string }[],
};
type GetRolesResponseDataModel = {
  total: number,
  data: {
    id: string,
    name: string,
    displayName: string
  }[]
};
type CreateRolePayload = {
  name: string,
  displayName: string,
  permissionIds: string[]
};
type EditRolePayload = {
  name: string,
  displayName: string,
  permissionIds: string[]
};
type CreateRoleResponseModel = {
  id: string
};
type EditRoleResponseModel = {
  id: string
};
export const getRoles = async (
  page: number = 1,
  pageSize: number = 10,
  search: string = ''
): Promise<GetRolesResponseDataModel> => {
  let res = await api.get<GetRolesResponseDataModel>('/role/all', {
    params: {
      page,
      pageSize,
      search,
    },
  });
  return res.data;

}

export const getRoleById = async (id: string): Promise<GetRoleByIdResponseDataModel> => {
  let res = await api.get<GetRoleByIdResponseDataModel>(`/roles/${id}`);
  return res.data;
}

export const createRole = async (data: CreateRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.post<CreateRoleResponseModel>('/roles', data);
  return res.data;
}

export const editRole = async (id: string, data: EditRolePayload): Promise<EditRoleResponseModel> => {
  let res = await api.put<EditRoleResponseModel>(`/roles/${id}`, data);
  return res.data;
} 