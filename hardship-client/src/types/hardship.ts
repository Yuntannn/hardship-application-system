export interface CreateHardshipApplicationRequest{
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    email: string;
    phone?: string;
    income: number;
    expenses: number;
    hardshipReason?: string;
}

export interface UpdateHardshipApplicationRequest{
    income: number;
    expenses: number;
    hardshipReason?: string;
    status: string;
}

export interface HardshipApplicationResponse{
    id: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    email: string;
    phone?: string;
    income: number;
    expenses: number;
    hardshipReason?: string;
    status: string;
    createdAt: string;
    updatedAt?: string;
}