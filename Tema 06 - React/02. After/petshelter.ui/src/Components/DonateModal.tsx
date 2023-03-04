import React, { useState } from "react";
import {
    Box,
    Button,
    Modal,
    TextField,
    Typography
} from "@mui/material";
import Donation from "../Models/Donation";
import { RealPerson } from "../Models/RealPerson";

interface IDonateModalProps {
    open: boolean;
    close: () => void;
    fundraiserId: number;
    donate: (id: number, donation: Donation) => Promise<boolean>;
    refresh: () => void;
}

const DonateModal = ({ open, close, donate, fundraiserId, refresh }: IDonateModalProps) => {
    const [amount, setAmount] = useState<number>(0);
    const [name, setName] = useState("");
    const [idNumber, setIdNumber] = useState("");
    const [dateOfBirth, setdateOfBirth] = useState('');
    const [error, setError] = useState('');
    const isAdult = (date: Date): boolean => {
        var ageDifMs = Date.now() - date.getTime();
        var ageDate = new Date(ageDifMs); // miliseconds from epoch
        if((Math.abs(ageDate.getUTCFullYear() - 1970))>=18){
            return true;
        }
        return false;
    }
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError('')
        if (name.length < 3) {
            setError('Name length must be >2')
            return;
        }
        if (idNumber.length !== 13) {
            setError('Id number must be 13 characters in length')
            return;
        }
        if (!isAdult(new Date(dateOfBirth))) {
            setError('Person must be above 18 years old.')
            return;
        }
        donate(fundraiserId, new Donation(amount, new RealPerson(name, idNumber, new Date(dateOfBirth)))).then(data => {
            if (data) {
                refresh()
            }
        });

        setAmount(0);
        setName('');
        setIdNumber('');
        setdateOfBirth('');
        setError('');
        close();
    };

    return (
        <Modal open={open} onClose={close}>
            <Box
                sx={{
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    transform: "translate(-50%, -50%)",
                    bgcolor: "background.paper",
                    boxShadow: 24,
                    p: 4,
                    maxWidth: 400,
                    borderRadius: 2,
                }}
            >
                <Typography variant="h6" component="h2" gutterBottom>
                    Donate
                </Typography>
                <form onSubmit={handleSubmit}>
                    <TextField
                        required
                        fullWidth
                        label="Amount"
                        type="number"
                        value={amount}
                        onChange={(e) => setAmount(Number(e.target.value))}
                        sx={{ mb: 2 }}
                    />
                    <TextField
                        required
                        fullWidth
                        label="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        sx={{ mb: 2 }}
                    />
                    <TextField
                        required
                        fullWidth
                        label="Card ID"
                        value={idNumber}
                        onChange={(e) => setIdNumber(e.target.value)}
                        sx={{ mb: 2 }}
                    />
                    <TextField
                        required
                        fullWidth
                        // label="Birthdate"
                        type="date"
                        value={dateOfBirth}
                        onChange={(e) => setdateOfBirth(e.target.value)}
                        sx={{ mb: 2 }}
                    />
                    <Typography sx={{ color: 'red', fontSize: '1rem', fontWeight: 'bold' }}>
                        {error}
                    </Typography>

                    <Button type="submit" variant="contained" color="primary">
                        Donate
                    </Button>
                </form>
            </Box>
        </Modal>
    );
};

export default DonateModal;