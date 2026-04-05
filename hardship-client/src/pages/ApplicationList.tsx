import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getApplications } from "../api/hardshipApi";
import type { HardshipApplicationResponse } from "../types/hardship";
import { Button, Table, Alert, Typography, Space } from "antd";

const { Title } = Typography;

export default function ApplicationList() {
    const [applications, setApplications] = useState<HardshipApplicationResponse[]>([]);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        getApplications()
            .then(setApplications)
            .catch(() => setError('Failed to load applications.'));
    }, []);

    const columns = [
        {title: 'Name', render: (_: unknown, app: HardshipApplicationResponse) => `${app.firstName} ${app.lastName}`},
        {title: 'Email', dataIndex: 'email'},
        {title: 'Phone', dataIndex: 'phone'},
        {title: 'Expenses', dataIndex: 'expenses'},
        {title: 'Status', dataIndex: 'status'},
        {title: 'Actions',
            render: (_: unknown, app: HardshipApplicationResponse) => (
                <Button onClick={() => navigate(`/edit/${app.id}`)}>Edit</Button>
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