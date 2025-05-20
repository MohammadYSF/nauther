import api from './api';
type CreatePermissionPayload = {
  name: string,
  displayName: string
};
type EditPermissionPayload = {
  name: string,
  displayName: string
};
type CreatePermissionResponseDataModel = {
  id: string
};
type EditPermissionResponseDataModel = {
  id: string
}
type GetPermissionsResponseDataModel = {
  total: number,
  data: {
    id: string,
    name: string,
    displayName: string
  }[]
};
type GetPermissionByIdResponseDataModel = {
  id: string,
  name: string,
  displayName: string
};

export const getPermissions = async (
  page: number = 1,
  pageSize: number = 10,
  search: string = ''
) => {
  let res = await api.get<GetPermissionsResponseDataModel>('/permissions', {
    params: {
      page,
      pageSize,
      search,
    },
  });
  return res.data;
}
export const getPermissionById = async (id: string) => {
  let res = await api.get<GetPermissionByIdResponseDataModel>(`/permissions/${id}`);
  return res.data;
}
export const createPermission = async (data: CreatePermissionPayload) => {

  let res = await api.post<CreatePermissionResponseDataModel>('/permissions', data);
  res.data;
}
export const editPermission = async (id: string, data: EditPermissionPayload) => {
  let res = await api.put<EditPermissionResponseDataModel>(`/permissions/${id}`, data);
  return res.data;
} 
export type {EditPermissionResponseDataModel,CreatePermissionResponseDataModel,CreatePermissionPayload,
  EditPermissionPayload,
  GetPermissionByIdResponseDataModel,GetPermissionsResponseDataModel
}