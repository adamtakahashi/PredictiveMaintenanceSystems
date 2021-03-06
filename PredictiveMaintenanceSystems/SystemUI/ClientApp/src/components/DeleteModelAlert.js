import React from 'react';
import {withStyles} from '@material-ui/core/styles';
import {Alert, AlertTitle} from '@material-ui/lab';
import IconButton from '@material-ui/core/IconButton';
import Collapse from '@material-ui/core/Collapse';
import CloseIcon from '@material-ui/icons/Close';

const useStyles = (theme) => ({
    root: {
        width: '100%',
        '& > * + *': {
            marginTop: theme.spacing(2),
        },
    },
});

//Show a confirmation message when deleting a model
class DeleteModelAlert extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: true,
            status: this.props.status,
        };
        this.setOpen=this.setOpen.bind(this);
    }

    setOpen(value) {
        this.setState({open: value});
    }

    //Renders the alert based on the code passed to it
    render() {
        const {classes} = this.props;
        if(this.state.status === "success") {
            return (
                <div className={classes.root}>
                    <Collapse in={this.state.open}>
                        <Alert severity="success"
                               action={
                                   <IconButton
                                       aria-label="close"
                                       color="inherit"
                                       size="medium"
                                       onClick={() => {
                                           this.setOpen(false);
                                       }}
                                   >
                                       <CloseIcon fontSize="inherit" />
                                   </IconButton>
                               }
                        ><AlertTitle>Success!</AlertTitle>
                            Model has been deleted.
                        </Alert>
                    </Collapse>
                </div>
            );
        }
        else if (this.state.status === "error") {
            return (
                <div className={classes.root}>
                    <Collapse in={this.state.open}>
                        <Alert severity="error"
                               action={
                                   <IconButton
                                       aria-label="close"
                                       color="inherit"
                                       size="medium"
                                       onClick={() => {
                                           this.setOpen(false);
                                       }}
                                   >
                                       <CloseIcon fontSize="inherit" />
                                   </IconButton>
                               }
                        ><AlertTitle>Error</AlertTitle>
                            Model has not been deleted.
                        </Alert>
                    </Collapse>
                </div>
            );
        }
        else if (this.state.status === "noModelSelected") {
            return (
                <div className={classes.root}>
                    <Collapse in={this.state.open}>
                        <Alert severity="error"
                               action={
                                   <IconButton
                                       aria-label="close"
                                       color="inherit"
                                       size="medium"
                                       onClick={() => {
                                           this.setOpen(false);
                                       }}
                                   >
                                       <CloseIcon fontSize="inherit" />
                                   </IconButton>
                               }
                        ><AlertTitle>Error</AlertTitle>
                            You do not have a model selected.
                        </Alert>
                    </Collapse>
                </div>
            );
        }
    }
}

export default withStyles(useStyles)(DeleteModelAlert);