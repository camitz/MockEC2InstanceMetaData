This application may be used in conjunction with a proxy request manipulator like Fiddler to emulate reponses from the AWS [instance meta data service](http://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-instance-metadata.html). Use this to debug locally any application which depends on intance meta data. 

The reponses are completely configurable via a json file. The app does not have to be restarted after changed to this file.

The application is run as an Owin self hosted web listener, listening on *http://127.0.0.1:9000*. The port is configurable.

##Setting up Fiddler

In order to redirect the requests from *169.254.169.254* you must configure a proxy service, I recommend [Fiddler2](http://www.telerik.com/fiddler). Goto Tools->Hosts and add the following line:

    127.0.0.1:9000 169.254.169.254

##Configuring the response

A json text file, *metadata.json*, may be placed in the executable directory or any parent. Below is a sample.

    {
        "latest": {
            "meta-data": {
                "instance-id": "mock-instance-id",
                "spot": {
                    "termination-time": "2015-01-07 00:00:00"
                }
            }
        }
    }
