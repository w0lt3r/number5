export interface Permission{
    id: number,
    name: string,
    lastName: string,
    permissionTypeId: number,
    permissionTypeDescription: string,
    effectiveFrom: Date
}