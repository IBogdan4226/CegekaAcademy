import React, { useState } from 'react'
import { Fundraiser, FundraiserStatus } from '../Models/Fundraiser';
import { Button, Card, CardActions, CardContent, Tooltip, Typography } from "@mui/material";
import Diversity1Icon from '@mui/icons-material/Diversity1';
import Donation from './../Models/Donation';
import DonateModal from './DonateModal';
import { useEffect } from 'react';
export interface IFundraiserProps {
    fundraiser: Fundraiser;
    handleSubmit: (id: number, donation: Donation) => Promise<boolean>;
    handleRefresh: () => void;
}
const FundraiserCard = (props: IFundraiserProps) => {
    const [open, setOpen] = useState(false);
    const [canDonate, setCanDonate] = useState(true);

    useEffect(() => {
        if (props.fundraiser.status === FundraiserStatus.Closed
            ||
            (new Date(props.fundraiser.dueDate).getTime()) - Date.now() < 0) {
            setCanDonate(false);
        }
    })
    const handleDonate = () => {
        setOpen(true);
    }

    return (
        <>
            <Card sx={{ maxWidth: 345 }}>

                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.fundraiser.name} &nbsp;
                        {

                            canDonate &&
                            <Tooltip title={`${props.fundraiser.name} is closed.`}><Diversity1Icon></Diversity1Icon></Tooltip>
                        }
                    </Typography>
                    <Typography component={'span'} variant="body2" color="text.secondary">
                        <p>Creation date <>{props.fundraiser.creationDate}</></p>
                    </Typography>
                    <Typography component={'span'} variant="body2" color="text.secondary">
                        <p>Due date <>{props.fundraiser.dueDate}</></p>
                    </Typography>
                    <Typography component={'span'} variant="body2" color="text.secondary">
                        <p>Currently raised {props.fundraiser.currentlyRaised} out of {props.fundraiser.target}</p>
                    </Typography>
                </CardContent>
                <CardActions sx={{ float: "right" }}>
                    <Button size="small">Learn More</Button>
                    {
                        canDonate &&
                        <Button size="small" color='primary' variant="contained" onClick={handleDonate}>Donate</Button>
                    }
                </CardActions>
            </Card>

            <DonateModal open={open} close={() => { setOpen(!open) }} donate={props.handleSubmit} refresh={props.handleRefresh} fundraiserId={props.fundraiser.id || 0}></DonateModal>
        </>
    )
}

export default FundraiserCard