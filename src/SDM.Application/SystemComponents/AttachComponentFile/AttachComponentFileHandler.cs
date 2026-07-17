using SDM.Application.Common;
using SDM.Application.Common.CQRS;
using SDM.Application.Interfaces;
using SDM.Domain.Entities;

namespace SDM.Application.SystemComponents.AttachComponentFile;

/// <summary>Handles <see cref="AttachComponentFileCommand"/>.</summary>
public sealed class AttachComponentFileHandler : ICommandHandler<AttachComponentFileCommand, AttachComponentFileResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>Initializes a new instance of <see cref="AttachComponentFileHandler"/>.</summary>
    public AttachComponentFileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result<AttachComponentFileResponse>> Handle(AttachComponentFileCommand command, CancellationToken cancellationToken)
    {
        var componentExists = await _unitOfWork.SystemComponents.ExistsAsync(command.ComponentId, cancellationToken);
        if (!componentExists)
            return Result<AttachComponentFileResponse>.Failure(Error.NotFound("SystemComponent"));

        var file = new ComponentFile(
            command.ComponentId,
            command.FileType,
            command.StoredFileName,
            command.OriginalFileName,
            command.RelativePath,
            command.FileSize,
            command.MimeType,
            command.SHA256,
            command.Version);

        await _unitOfWork.ComponentFiles.AddAsync(file, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AttachComponentFileResponse>.Success(
            new AttachComponentFileResponse { FileId = file.Id },
            "File attached to system component successfully.");
    }
}
