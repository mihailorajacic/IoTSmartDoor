using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Services
{
    public interface IFaceAPIService
    {
        Task<Face[]> DetectAsync(string imageUrl);

        Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string imageUrl, string faceListId);
    }
}
