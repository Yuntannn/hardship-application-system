import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getApplicationById, updateApplication } from "../api/hardshipApi";
import { ApplicationStatus, type UpdateHardshipApplicationRequest } from "../types/hardship";
import { Button, Form, Input, InputNumber, Alert, Typography, Select } from "antd";

const { Title } = Typography;

export default function UpdateApplication() {
    const { id } = useParams<{id: string}>();
    const navigate = useNavigate();
    const [error, setError] = useState<string | null>(null);
    const [form] = Form.useForm();

    useEffect(() => {
        if(!id) return;
        getApplicationById(id)
            .then(app => form.setFieldsValue({
                income: app.income,
                expenses: app.expenses,
                hardshipReason: app.hardshipReason ?? '',
                status: app.status,
            }))
            .catch(() => setError('Failed to load application.'));
    }, [id]);


    const handleSubmit = async (values: UpdateHardshipApplicationRequest) => {
        try {
            await updateApplication(id!, values);
            navigate('/');
        }catch{
            setError('Failed to load application. Please try again.');
        }
    };

    return (
        <div style={{ padding: 32, maxWidth: 600, margin: '0 auto' }}>
            <Title level={2}>Edit Hardship Application</Title>
            {error && <Alert description={error} type="error" style={{ marginBottom: 16 }} />}

            <Form form={form} layout="vertical" onFinish={handleSubmit}>
                <Form.Item name="income" label="Income" rules={[{ required: true }]}>
                    <InputNumber style={{ width: '100%' }} min={0} />
                </Form.Item>
                <Form.Item name="expenses" label="Expenses" rules={[{ required: true }]}>
                    <InputNumber style={{ width: '100%' }} min={0} />
                </Form.Item>
                <Form.Item name="hardshipReason" label="Hardship Reason">
                    <Input.TextArea rows={3} />
                </Form.Item>
                <Form.Item name="status" label="Status" rules={[{ required: true }]}>
                    <Select>
                        <Select.Option value={ApplicationStatus.Pending}>Pending</Select.Option>
                        <Select.Option value={ApplicationStatus.UnderReview}>Under Review</Select.Option>
                        <Select.Option value={ApplicationStatus.Approved}>Approved</Select.Option>
                        <Select.Option value={ApplicationStatus.Declined}>Declined</Select.Option>
                    </Select>
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" style={{ marginRight: 8 }}>Update</Button>
                    <Button onClick={() => navigate('/')}>Cancel</Button>
                </Form.Item>
            </Form>
        </div>
    );

    
}
