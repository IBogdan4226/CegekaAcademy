import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FundraiserService } from "../Services/FundraiserService";
import FundraiserCard from "../Components/FundraiserCard";
import { Fundraiser } from "../Models/Fundraiser";
import Donation from "../Models/Donation";
export const Fundraisers = () => {
    const fundraiserService=new FundraiserService();
    const [fundraiser,setFundraiser]=useState<Fundraiser[]>([])

    const retrieveData=()=>{
        fundraiserService.getAll().then(data=>{
            setFundraiser(data)
        })
    }
    useEffect(()=>{
        retrieveData();
        
    },[])

    const donateToFundraiser=(id:number,donation:Donation):Promise<boolean>=>{
        return fundraiserService.donateToFundraiser(id,donation);
    }
  return (
    <Fragment>
            <Box>
                <Button>
                    <Link to="/">Go to the home page</Link>
                </Button>
            </Box>
            <Container>

                <Grid container spacing={4}>
                    {
                        fundraiser.map((fundraiser,index) => (
                            <Grid item key={index} xs={12} sm={6} md={4}>
                                <FundraiserCard fundraiser={fundraiser} handleSubmit={donateToFundraiser} handleRefresh={retrieveData}></FundraiserCard>
                            </Grid>
                        ))
                    }
                </Grid>
            </Container>
        </Fragment>
  )
}
