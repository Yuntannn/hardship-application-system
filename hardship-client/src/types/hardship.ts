export const ApplicationStatus = {
    Pending: 0,
    UnderReview: 1,
    Approved: 2,
    Declined: 3,
} as const;

export type ApplicationStatus = (typeof ApplicationStatus)[keyof typeof ApplicationStatus];

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
    status: ApplicationStatus;
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
    status: ApplicationStatus;
    createdAt: string;
    updatedAt?: string;
}
