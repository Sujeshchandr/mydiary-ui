function DRError(data) {

   
    self = this;
    self.Type = "Generic Error";
    self.Message = "undefined";

    if (typeof data == "string") {
        self.Type = "Error";
        self.Message = data;

    }
    else if(typeof data == "object"){
        self.Type = data.Type;
        self.Message = data.Message;

    }

    DRLog.error(self);
   
};