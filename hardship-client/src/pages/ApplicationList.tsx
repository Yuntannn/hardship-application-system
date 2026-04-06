import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getApplications, deleteApplication  } from "../api/hardshipApi";
import { ApplicationStatus, type HardshipApplicationResponse } from "../types/hardship";
import { Button, Table, Alert, Typography, Space } from "antd";

const { Title } = Typography;

const statusLabels: Record<ApplicationStatus, string> = {
    [ApplicationStatus.Pending]: "Pending",
    [ApplicationStatus.UnderReview]: "Under Review",
    [ApplicationStatus.Approved]: "Approved",
    [ApplicationStatus.Declined]: "Declined",
};

export default function ApplicationList() {
    const [applications, setApplications] = useState<HardshipApplicationResponse[]>([]);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        getApplications()
            .then(setApplications)
            .catch(() => setError('Failed to load applications.'));
    }, []);

    const handleDelete = async (id: string) => {
        try {
            await deleteApplication(id);
            setApplications(prev => prev.filter(app => app.id !== id));
        } catch {
            setError('Failed to delete application.');
        }
    };

    const columns = [
        {title: 'Name', render: (_: unknown, app: HardshipApplicationResponse) => `${app.firstName} ${app.lastName}`},
        {title: 'Email', dataIndex: 'email'},
        {title: 'Phone', dataIndex: 'phone'},
        {title: 'Expenses', dataIndex: 'expenses'},
        {title: 'Status', render: (_: unknown, app: HardshipApplicationResponse) => statusLabels[app.status]},
        {title: 'Actions',
            render: (_: unknown, app: HardshipApplicationResponse) => (
                <Space>
                    <Button onClick={() => navigate(`/edit/${app.id}`)}>Edit</Button>
                    <Button danger onClick={() => handleDelete(app.id)}>Delete</Button>
                </Space>
            ),
        },
    ];

    return (
        <div style={{padding: 32}}>
            <Space style={{width: '100%', justifyContent: 'space-between', marginBottom: 16}}>
                <Title level={2}>Hardship Applications</Title>
                <Button type="primary" onClick={() => navigate('/create')}>New Application</Button>
            </Space>
            {error && <Alert description={error} type="error" style={{marginBottom: 16}} />}
            <Table dataSource={applications} columns={columns} rowKey="id" />
        </div>
    );
}
